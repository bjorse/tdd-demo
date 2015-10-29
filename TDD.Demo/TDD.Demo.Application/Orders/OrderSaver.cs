using System;
using TDD.Demo.Application.Queries;
using TDD.Demo.Application.Shipments;
using TDD.Demo.Application.Util;
using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Application.Orders
{
    public class OrderSaver : IOrderSaver
    {
        private readonly IDbContextFactory _contextFactory;
        private readonly IEntitySaver _entitySaver;
        private readonly IShipmentOrderItemsUpdater _shipmentOrderItemsUpdater;

        public OrderSaver(IDbContextFactory contextFactory,
                          IEntitySaver entitySaver,
                          IShipmentOrderItemsUpdater shipmentOrderItemsUpdater)
        {
            _contextFactory = contextFactory;
            _entitySaver = entitySaver;
            _shipmentOrderItemsUpdater = shipmentOrderItemsUpdater;
        }

        public OrderSavedResult SaveOrder(OrderModel order)
        {
            using (var context = _contextFactory.Create())
            {
                var latestOrderShipment = context.Shipments.GetLatestByOrderNumber(order.OrderNumber);

                if (latestOrderShipment != null && latestOrderShipment.IsShipped)
                {
                    throw new ArgumentException(string.Format("This order cannot be changed because it has been shipped!"));
                }

                var savedOrder = _entitySaver.Save(order, RevisionPolicy.OnlyNewRevision);

                if (latestOrderShipment != null)
                {
                    latestOrderShipment.OrderRevision = savedOrder.Id;
                    latestOrderShipment.Items = _shipmentOrderItemsUpdater.UpdateOrderItems(latestOrderShipment.Items, savedOrder.Items);

                    _entitySaver.Save(latestOrderShipment, RevisionPolicy.OnlyNewRevision);
                }

                context.SaveChanges();

                return new OrderSavedResult
                {
                    CustomerId = savedOrder.OrderInfo.CustomerId,
                    OrderNumber = savedOrder.OrderNumber,
                    RevisionId = savedOrder.Id
                };
            }
        }
    }
}

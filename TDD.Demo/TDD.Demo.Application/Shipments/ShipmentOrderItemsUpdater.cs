using System.Collections.Generic;
using System.Linq;
using TDD.Demo.Domain.Orders;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Application.Shipments
{
    public class ShipmentOrderItemsUpdater : IShipmentOrderItemsUpdater
    {
        public IList<OrderItemShipmentModel> UpdateOrderItems(IEnumerable<OrderItemShipmentModel> currentOrderItems, IEnumerable<OrderItemModel> updatedOrderItems)
        {
            var updatedOrderItemsList = updatedOrderItems.ToArray();
            var currentOrderItemsList = currentOrderItems.ToArray();
            var currentItems = currentOrderItemsList.Select(x => x.OrderItem.Item.Id).ToArray();
            var remainingItems = updatedOrderItemsList.Select(x => x.Item.Id).ToArray();
            var newItems = remainingItems.Where(x => !currentItems.Contains(x));
            var remainingOrderItems = FilterRemainingItems(currentOrderItemsList, remainingItems).ToArray();

            foreach (var item in remainingOrderItems)
            {
                item.OrderItem = updatedOrderItemsList.First(x => x.Item.Id == item.OrderItem.Item.Id);
            }

            return remainingOrderItems.Concat(CreateNewOrderItems(updatedOrderItemsList.Where(x => newItems.Contains(x.Item.Id)))).ToList();
        }

        private static IEnumerable<OrderItemShipmentModel> FilterRemainingItems(IEnumerable<OrderItemShipmentModel> orderItems, IEnumerable<int> remainingItemIds)
        {
            return orderItems.Where(x => remainingItemIds.Contains(x.OrderItem.Item.Id));
        }

        private static IEnumerable<OrderItemShipmentModel> CreateNewOrderItems(IEnumerable<OrderItemModel> orderItems)
        {
            return orderItems.Select(x => new OrderItemShipmentModel {OrderItem = x});
        }
    }
}

using System.Collections.Generic;
using TDD.Demo.Domain.Orders;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Application.Shipments
{
    public interface IShipmentOrderItemsUpdater
    {
        IList<OrderItemShipmentModel> UpdateOrderItems(IEnumerable<OrderItemShipmentModel> currentOrderItems, IEnumerable<OrderItemModel> updatedOrderItems);
    }
}

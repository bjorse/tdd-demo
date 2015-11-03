using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ChangedOrderItemsLoader : IChangedOrderItemsLoader
    {
        private readonly IShipmentService _shipmentService;

        public ChangedOrderItemsLoader(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public async Task<IEnumerable<ChangedOrderItemResult>> GetChangedOrderItemsAsync(OrderShipmentModel shipmentModel)
        {
            var previousShipment = await _shipmentService.GetPreviousShipmentWithAnotherOrderRevisionAsync(shipmentModel.Id);

            if (previousShipment == null)
            {
                return new ChangedOrderItemResult[0];
            }

            return GetChangedOrderItems(shipmentModel.Items, previousShipment.Items)
                   .Concat(GetRemovedOrderItems(shipmentModel.Items, previousShipment.Items))
                   .Where(HasChanged);
        }

        private static IEnumerable<ChangedOrderItemResult> GetChangedOrderItems(IEnumerable<OrderItemShipmentModel> currentItems, IEnumerable<OrderItemShipmentModel> previousItems)
        {
            return from currentOrderItem in currentItems
                   let previousOrderItem = previousItems.FirstOrDefault(x => x.OrderItem.Item.Id == currentOrderItem.OrderItem.Item.Id)
                   select new ChangedOrderItemResult
                   {
                       Item = currentOrderItem.OrderItem.Item,
                       PreviousQuantity = previousOrderItem != null ? previousOrderItem.OrderItem.Quantity : (int?) null,
                       CurrentQuantity = currentOrderItem.OrderItem.Quantity,
                       New = previousOrderItem == null
                   };
        }

        private static IEnumerable<ChangedOrderItemResult> GetRemovedOrderItems(IEnumerable<OrderItemShipmentModel> currentItems, IEnumerable<OrderItemShipmentModel> previousItems)
        {
            var currentItemIds = currentItems.Select(x => x.OrderItem.Item.Id);

            return previousItems.Where(x => !currentItemIds.Contains(x.OrderItem.Item.Id))
                                .Select(previousOrderItem => new ChangedOrderItemResult
                                {
                                    Item = previousOrderItem.OrderItem.Item,
                                    PreviousQuantity = previousOrderItem.OrderItem.Quantity,
                                    CurrentQuantity = null,
                                    RemovedFromOrder = true,
                                    RemovedFromOrderAndNeedsUnpacking = previousOrderItem.IsPackaged
                                });
        }

        private static bool HasChanged(ChangedOrderItemResult result)
        {
            return result.PreviousQuantity != result.CurrentQuantity || result.RemovedFromOrder ||
                   result.RemovedFromOrderAndNeedsUnpacking || result.New;
        }
    }
}

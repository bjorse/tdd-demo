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

            var result = new List<ChangedOrderItemResult>();

            foreach (var currentOrderItem in shipmentModel.Items)
            {
                var previousOrderItem = previousShipment.Items.FirstOrDefault(x => x.OrderItem.Item.Id == currentOrderItem.OrderItem.Item.Id);

                result.Add(new ChangedOrderItemResult
                {
                    Item = currentOrderItem.OrderItem.Item,
                    PreviousQuantity = previousOrderItem != null ? previousOrderItem.OrderItem.Quantity : (int?)null,
                    CurrentQuantity = currentOrderItem.OrderItem.Quantity,
                    New = previousOrderItem == null
                });
            }

            var currentItemIds = shipmentModel.Items.Select(x => x.OrderItem.Item.Id);
            var removedOrderItems = previousShipment.Items.Where(x => !currentItemIds.Contains(x.OrderItem.Item.Id));

            foreach (var previousOrderItem in removedOrderItems)
            {
                result.Add(new ChangedOrderItemResult
                {
                    Item = previousOrderItem.OrderItem.Item,
                    PreviousQuantity = previousOrderItem.OrderItem.Quantity,
                    CurrentQuantity = null,
                    RemovedFromOrder = true,
                    RemovedFromOrderAndNeedsUnpacking = previousOrderItem.IsPackaged
                });
            }

            return result.Where(HasChanged);
        }

        private static bool HasChanged(ChangedOrderItemResult result)
        {
            return result.PreviousQuantity != result.CurrentQuantity || result.RemovedFromOrder ||
                   result.RemovedFromOrderAndNeedsUnpacking || result.New;
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ShipmentLoader : IShipmentLoader
    {
        private readonly ICustomerService _customerService;
        private readonly IShipmentService _shipmentService;
        private readonly IChangedOrderItemsLoader _changedOrderItemsLoader;
        private readonly IChangedOrderItemsInformationLoader _changedOrderItemsInformationLoader;

        public ShipmentLoader(ICustomerService customerService, 
                              IShipmentService shipmentService,
                              IChangedOrderItemsLoader changedOrderItemsLoader,
                              IChangedOrderItemsInformationLoader changedOrderItemsInformationLoader)
        {
            _customerService = customerService;
            _shipmentService = shipmentService;
            _changedOrderItemsLoader = changedOrderItemsLoader;
            _changedOrderItemsInformationLoader = changedOrderItemsInformationLoader;
        }

        public async Task<ShipmentLoadResult> LoadAsync(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            var shipmentOrders = await _shipmentService.GetAllNotShippedShipmentsByCustomerIdAsync(customerId);
            var ordersToShipResult = await Task.WhenAll(shipmentOrders.Select(GetShipmentLoadResult));

            return new ShipmentLoadResult
            {
                Customer = customer,
                OrdersToShip = new ObservableCollection<OrderShipmentLoadResult>(ordersToShipResult)
            };
        }

        private async Task<OrderShipmentLoadResult> GetShipmentLoadResult(OrderShipmentModel model)
        {
            var changedOrderItems = (await _changedOrderItemsLoader.GetChangedOrderItemsAsync(model)).ToArray();
            var changedOrderItemsInformation = _changedOrderItemsInformationLoader.GetChangedOrderItemInformation(changedOrderItems);

            return new OrderShipmentLoadResult
            {
                Model = model,
                ChangedOrderItems = changedOrderItems,
                ChangedOrderInformation = changedOrderItemsInformation
            };
        }
    }
}

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ShipmentLoader : IShipmentLoader
    {
        private readonly ICustomerService _customerService;
        private readonly IShipmentService _shipmentService;

        public ShipmentLoader(ICustomerService customerService, IShipmentService shipmentService)
        {
            _customerService = customerService;
            _shipmentService = shipmentService;
        }

        public async Task<ShipmentLoadResult> LoadAsync(int customerId)
        {
            var customer = await _customerService.GetCustomerById(customerId);
            var shipmentOrders = await _shipmentService.GetAllNotShippedShipmentsByCustomerIdAsync(customerId);

            return new ShipmentLoadResult
            {
                Customer = customer,
                OrdersToShip = new ObservableCollection<OrderShipmentModel>(shipmentOrders)
            };
        }
    }
}

using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments
{
    public interface IOrderShipmentViewModelFactory
    {
        IOrderShipmentViewModel CreateOrderShipmentViewModel(CustomerModel customer, OrderShipmentModel model);
    }
}

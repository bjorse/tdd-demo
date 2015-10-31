using TDD.Demo.Presentation.Shipments.Savers;

namespace TDD.Demo.Presentation.Shipments
{
    public class OrderShipmentViewModelFactory : IOrderShipmentViewModelFactory
    {
        private readonly IOrderListItemViewModelFactory _orderListItemViewModelFactory;
        private readonly IShipmentSaver _saver;

        public OrderShipmentViewModelFactory(IOrderListItemViewModelFactory orderListItemViewModelFactory, IShipmentSaver saver)
        {
            _orderListItemViewModelFactory = orderListItemViewModelFactory;
            _saver = saver;
        }

        public IOrderShipmentViewModel CreateOrderShipmentViewModel()
        {
            return new OrderShipmentViewModel(_orderListItemViewModelFactory, _saver);
        }
    }
}

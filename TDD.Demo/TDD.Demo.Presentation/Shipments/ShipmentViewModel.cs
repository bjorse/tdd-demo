using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TDD.Demo.Domain;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;

namespace TDD.Demo.Presentation.Shipments
{
    public class ShipmentViewModel : NotificationObject, IShipmentViewModel
    {
        private readonly IOrderShipmentViewModelFactory _viewModelFactory;
        private readonly IShipmentLoader _loader;

        public ShipmentViewModel(IOrderShipmentViewModelFactory viewModelFactory, IShipmentLoader loader)
        {
            _viewModelFactory = viewModelFactory;
            _loader = loader;
        }

        public async Task InitializeAsync(int customerId)
        {
            var loadResult = await _loader.LoadAsync(customerId);
            var viewModels = loadResult.OrdersToShip.Select(x => CreateOrderShipmentViewModel(loadResult.Customer, x));

            OrderShipments = new ObservableCollection<IOrderShipmentViewModel>(viewModels);
        }

        private IList<IOrderShipmentViewModel> _orderShipments = new ObservableCollection<IOrderShipmentViewModel>();

        public IList<IOrderShipmentViewModel> OrderShipments
        {
            get { return _orderShipments; }
            set
            {
                if (Equals(_orderShipments, value))
                {
                    return;
                }

                _orderShipments = value;
                RaisePropertyChanged();
            }
        }

        private IOrderShipmentViewModel CreateOrderShipmentViewModel(CustomerModel customer, OrderShipmentLoadResult orderShipment)
        {
            var viewModel = _viewModelFactory.CreateOrderShipmentViewModel();
            viewModel.Initialize(customer, orderShipment.Model);

            return viewModel;
        }
    }
}

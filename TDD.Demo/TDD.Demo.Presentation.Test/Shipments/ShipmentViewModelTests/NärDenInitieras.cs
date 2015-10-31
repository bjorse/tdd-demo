using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentViewModelTests
{
    public class NärDenInitieras : GivetEnShipmentViewModel
    {
        private const int CustomerId = 18;

        private readonly IList<string> _raisedProperties = new List<string>();
 
        private CustomerModel _customer;
        private IEnumerable<OrderShipmentModel> _models;
        private IOrderShipmentViewModel _orderShipmentViewModel;

        protected override void Given()
        {
            base.Given();

            _customer = new CustomerModel {Id = CustomerId};
            _models = new[]
            {
                new OrderShipmentModel {Id = 1},
                new OrderShipmentModel {Id = 2},
                new OrderShipmentModel {Id = 3},
                new OrderShipmentModel {Id = 4},
                new OrderShipmentModel {Id = 5}
            };
            _orderShipmentViewModel = Substitute.For<IOrderShipmentViewModel>();

            ViewModel.PropertyChanged += (sender, args) => _raisedProperties.Add(args.PropertyName);
            Loader.LoadAsync(Arg.Any<int>()).Returns(Task.FromResult(new ShipmentLoadResult {Customer = _customer, OrdersToShip = _models.Select(x => new OrderShipmentLoadResult { Model = x }).ToList()}));
            ViewModelFactory.CreateOrderShipmentViewModel().Returns(_orderShipmentViewModel);
        }

        protected override void When()
        {
            ViewModel.InitializeAsync(CustomerId).GetAwaiter().GetResult();
        }

        [Then]
        public void SåHarLoadernBlivitAnropadKorrekt()
        {
            Loader.Received(1).LoadAsync(CustomerId);
        }

        [Then]
        public void SåHarViewModelFactoryBlivitAnropadKorrektAntalGånger()
        {
            ViewModelFactory.Received(5).CreateOrderShipmentViewModel();
        }

        [Then]
        public void SåHarViewModelBlivitAnropadMedKorrektModeller()
        {
            foreach (var model in _models)
            {
                _orderShipmentViewModel.Received(1).Initialize(_customer, model);
            }
        }

        [Then]
        public void SåInnehållerOrderShipmentsKorrektAntalElement()
        {
            Assert.AreEqual(5, ViewModel.OrderShipments.Count);
        }

        [Then]
        public void SåÄrOrderShipmentsObservableCollection()
        {
            Assert.IsTrue(ViewModel.OrderShipments.GetType() == typeof(ObservableCollection<IOrderShipmentViewModel>));
        }

        [Then]
        public void SåHarKorrektAntalPropertiesRaisats()
        {
            Assert.AreEqual(1, _raisedProperties.Count);
        }

        [Then]
        public void SåHarPropertynOrderShipmentsRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("OrderShipments"));
        }
    }
}

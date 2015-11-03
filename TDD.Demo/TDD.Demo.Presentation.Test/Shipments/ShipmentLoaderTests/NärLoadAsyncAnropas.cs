using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Items;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentLoaderTests
{
    public class NärLoadAsyncAnropas : GivetEnShipmentLoader
    {
        private const string ExpectedChangedOrderItemsInformation = "Några ordrar har ändrats!";

        private const int CustomerId = 38;

        private CustomerModel _customer;
        private IEnumerable<OrderShipmentModel> _ordersToShip;
        private IEnumerable<ChangedOrderItemResult> _changedOrderItems; 

        private ShipmentLoadResult _result;

        protected override void Given()
        {
            base.Given();

            var orderInfo = new OrderInfoModel {CustomerId = CustomerId, Id = 1};
            _customer = new CustomerModel {FirstName = "Kalle", LastName = "Kula", Id = CustomerId};
            _ordersToShip = new[]
            {
                new OrderShipmentModel {Id = 1, OrderInfo = orderInfo},
                new OrderShipmentModel {Id = 2, OrderInfo = orderInfo},
                new OrderShipmentModel {Id = 3, OrderInfo = orderInfo}
            };
            _changedOrderItems = new[]
            {
                new ChangedOrderItemResult {Item = new ItemModel {Id = 1}},
                new ChangedOrderItemResult {Item = new ItemModel {Id = 2}},
                new ChangedOrderItemResult {Item = new ItemModel {Id = 3}}
            };

            CustomerService.GetCustomerByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(_customer));
            ShipmentService.GetAllNotShippedShipmentsByCustomerIdAsync(Arg.Any<int>()).Returns(Task.FromResult(_ordersToShip));
            ChangedOrderItemsLoader.GetChangedOrderItemsAsync(Arg.Any<OrderShipmentModel>()).Returns(_changedOrderItems);
            ChangedOrderItemsInformationLoader.GetChangedOrderItemInformation(Arg.Any<IEnumerable<ChangedOrderItemResult>>()).Returns(ExpectedChangedOrderItemsInformation);
        }

        protected override void When()
        {
            _result = Loader.LoadAsync(CustomerId).GetAwaiter().GetResult();
        }

        [Then]
        public void SåSkaCustomerServiceBlivitAnropadKorrekt()
        {
            CustomerService.Received(1).GetCustomerByIdAsync(CustomerId);
        }

        [Then]
        public void SåSkaShipmentServiceBlivitAnropadKorrekt()
        {
            ShipmentService.Received(1).GetAllNotShippedShipmentsByCustomerIdAsync(CustomerId);
        }

        [Then]
        public void SåSkaChangedOrderItemsLoaderBlivitAnropadFörAllaModeller()
        {
            foreach (var model in _ordersToShip)
            {
                ChangedOrderItemsLoader.Received(1).GetChangedOrderItemsAsync(model);
            }
        }

        [Then]
        public void SåSkaChangedOrderItemsInformationLoaderBlivitAnropadFörAllaModeller()
        {
            ChangedOrderItemsInformationLoader.Received(3).GetChangedOrderItemInformation(Arg.Any<ChangedOrderItemResult[]>());
        }

        [Then]
        public void SåSkaDetVaraKorrektCustomerIResultatet()
        {
            Assert.AreEqual(_customer, _result.Customer);
        }

        [Then]
        public void SåSkaDetVaraKorrektAntalOrdersToShipIResultatet()
        {
            Assert.AreEqual(_ordersToShip.Count(), _result.OrdersToShip.Count);
        }

        [Then]
        public void SåSkaOrdersToShipVaraObservableCollection()
        {
            Assert.IsTrue(_result.OrdersToShip.GetType() == typeof(ObservableCollection<OrderShipmentLoadResult>));
        }

        [Then]
        public void SåSkaDetVaraKorrektElementIOrdersToShipIResultatet()
        {
            Assert.IsTrue(_ordersToShip.All(x => _result.OrdersToShip.Select(y => y.Model).Contains(x)));
        }

        [Then]
        public void SåSkaDetVaraKorrektChangedOrderItemsIResultatet()
        {
            Assert.IsTrue(_result.OrdersToShip.All(x => _changedOrderItems.SequenceEqual(x.ChangedOrderItems)));
        }

        [Then]
        public void SåSkaDetVaraKorrektChangedOrderItemsInformationIResultatet()
        {
            Assert.IsTrue(_result.OrdersToShip.All(x => x.ChangedOrderInformation == ExpectedChangedOrderItemsInformation));
        }
    }
}

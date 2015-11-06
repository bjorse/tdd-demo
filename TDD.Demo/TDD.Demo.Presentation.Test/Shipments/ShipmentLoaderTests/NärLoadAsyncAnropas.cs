using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentLoaderTests
{
    public class NärLoadAsyncAnropas : GivetEnShipmentLoader
    {
        private const int CustomerId = 38;

        private CustomerModel _customer;
        private IEnumerable<OrderShipmentModel> _ordersToShip;

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

            CustomerServiceMock.GetCustomerByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(_customer));
            ShipmentServiceMock.GetAllNotShippedShipmentsByCustomerIdAsync(Arg.Any<int>()).Returns(Task.FromResult(_ordersToShip));
        }

        protected override void When()
        {
            _result = Loader.LoadAsync(CustomerId).GetAwaiter().GetResult();
        }

        [Then]
        public void SåSkaCustomerServiceBlivitAnropadKorrekt()
        {
            CustomerServiceMock.Received(1).GetCustomerByIdAsync(CustomerId);
        }

        [Then]
        public void SåSkaShipmentServiceBlivitAnropadKorrekt()
        {
            ShipmentServiceMock.Received(1).GetAllNotShippedShipmentsByCustomerIdAsync(CustomerId);
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
            Assert.IsTrue(_result.OrdersToShip.GetType() == typeof(ObservableCollection<OrderShipmentModel>));
        }

        [Then]
        public void SåSkaDetVaraKorrektElementIOrdersToShipIResultatet()
        {
            Assert.IsTrue(_ordersToShip.All(x => _result.OrdersToShip.Contains(x)));
        }
    }
}

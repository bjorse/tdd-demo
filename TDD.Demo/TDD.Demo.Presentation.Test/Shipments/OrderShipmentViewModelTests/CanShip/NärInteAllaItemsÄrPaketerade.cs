using System.Collections.Generic;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Orders;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests.CanShip
{
    public class NärInteAllaItemsÄrPaketerade : GivetEnOrderShipmentViewModel
    {
        private bool _canShip;

        protected override void Given()
        {
            base.Given();

            var customer = new CustomerModel();
            var orderShipment = new OrderShipmentModel
            {
                Items = new List<OrderItemShipmentModel>
                {
                    new OrderItemShipmentModel
                    {
                        IsPackaged = false
                    },
                    new OrderItemShipmentModel
                    {
                        IsPackaged = true
                    }
                }
            };

            ViewModel.Initialize(customer, orderShipment, string.Empty);
        }

        protected override void When()
        {
            _canShip = ViewModel.CanShip;
        }

        [Then]
        public void SåSkaCanShipVaraFalse()
        {
            Assert.IsFalse(_canShip);
        }
    }
}

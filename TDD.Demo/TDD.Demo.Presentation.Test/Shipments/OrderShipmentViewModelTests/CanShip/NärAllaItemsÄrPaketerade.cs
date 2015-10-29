using System.Collections.Generic;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests.CanShip
{
    public class NärAllaItemsÄrPaketerade : GivetEnOrderShipmentViewModel
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
                        IsPackaged = true
                    },
                    new OrderItemShipmentModel
                    {
                        IsPackaged = true
                    },
                    new OrderItemShipmentModel
                    {
                        IsPackaged = true
                    }
                }
            };

            ViewModel.Initialize(customer, orderShipment);
        }

        protected override void When()
        {
            _canShip = ViewModel.CanShip;
        }

        [Then]
        public void SåSkaCanShipVaraTrue()
        {
            Assert.IsTrue(_canShip);
        }
    }
}

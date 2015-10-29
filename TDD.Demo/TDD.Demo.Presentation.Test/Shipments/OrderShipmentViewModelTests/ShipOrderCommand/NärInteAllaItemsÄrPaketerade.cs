using System.Collections.Generic;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests.ShipOrderCommand
{
    public class NärInteAllaItemsÄrPaketerade : GivetEnOrderShipmentViewModel
    {
        private bool _canExecute;

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

            ViewModel.Initialize(customer, orderShipment);
        }

        protected override void When()
        {
            _canExecute = ViewModel.ShipOrderCommand.CanExecute(null);
        }

        [Then]
        public void SåSkaDenInteGåAttExekvera()
        {
            Assert.IsFalse(_canExecute);
        }
    }
}

using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests.ShipOrderCommand
{
    public class NärAllaItemsÄrPaketerade : GivetEnOrderShipmentViewModel
    {
        private OrderShipmentModel _model;

        protected override void Given()
        {
            base.Given();

            var customer = new CustomerModel();
            _model = new OrderShipmentModel
            {
                ShippedDate = null,
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

            ViewModel.Initialize(customer, _model);
        }

        protected override void When()
        {
            ViewModel.ShipOrderCommand.Execute(null);
        }

        [Then]
        public void SåSkaDenGåAttExekvera()
        {
            Assert.IsTrue(ViewModel.ShipOrderCommand.CanExecute(null));
        }

        [Then]
        public void SåSkaShippedDateInteVaraNull()
        {
            Assert.IsNotNull(_model.ShippedDate);
        }

        [Then]
        public void SåSkaShippedDateVaraSattTillEttVärde()
        {
            Assert.AreNotEqual(default(DateTime), _model.ShippedDate);
        }

        [Then]
        public void SåSkaShipmentSaverBlivitAnropadKorrekt()
        {
            Saver.Received(1).SaveOrderShipmentAsync(_model);
        }
    }
}

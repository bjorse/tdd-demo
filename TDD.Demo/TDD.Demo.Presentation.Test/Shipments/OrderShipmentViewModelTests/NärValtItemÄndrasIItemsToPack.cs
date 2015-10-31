using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public class NärValtItemÄndrasIItemsToPack : GivetEnOrderShipmentViewModel
    {
        private IOrderListItemViewModel _item;

        protected override void Given()
        {
            base.Given();

            var orderShipment = new OrderShipmentModel
            {
                Items = new List<OrderItemShipmentModel>
                {
                    new OrderItemShipmentModel(),
                    new OrderItemShipmentModel(),
                    new OrderItemShipmentModel(),
                    new OrderItemShipmentModel(),
                    new OrderItemShipmentModel()
                }
            };

            ViewModel.Initialize(new CustomerModel(), orderShipment, string.Empty);

            _item = ViewModel.ItemsToPack.First();
            _item.IsSelected.Returns(true);
        }

        protected override void When()
        {
            _item.OnSelectionChanged += Raise.Event<Action<IOrderListItemViewModel>>(_item);
        }

        [Then]
        public void SåSkaInteSilentDeselectBlivitAnropadPåDenSomRaisadeEventet()
        {
            _item.DidNotReceive().SilentDeselect();
        }

        [Then]
        public void SåSkaSilentDeselectBlivitAnropadPåAllaAndraItems()
        {
            foreach (var item in ViewModel.ItemsToPack.Where(x => x != _item))
            {
                item.Received(1).SilentDeselect();
            }
        }
    }
}

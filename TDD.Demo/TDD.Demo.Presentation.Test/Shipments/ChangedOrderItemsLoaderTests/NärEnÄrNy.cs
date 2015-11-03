using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsLoaderTests
{
    public class NärEnÄrNy : GivetEnChangedOrderItemsLoader
    {
        private const int ShipmentId = 34;

        private OrderShipmentModel _previousModel;
        private OrderShipmentModel _currentModel;

        private ChangedOrderItemResult[] _result;

        protected override void Given()
        {
            base.Given();

            _previousModel = new OrderShipmentModel
            {
                Items = new List<OrderItemShipmentModel>()
            };

            _currentModel = new OrderShipmentModel()
            {
                Id = ShipmentId,
                Items = new List<OrderItemShipmentModel>
                {
                    new OrderItemShipmentModel {OrderItem = {Quantity = 4, Item = {Id = 1}}}
                }
            };

            ShipmentService.GetPreviousShipmentWithAnotherOrderRevisionAsync(Arg.Any<int>()).Returns(Task.FromResult(_previousModel));
        }

        protected override void When()
        {
            _result = Loader.GetChangedOrderItemsAsync(_currentModel).GetAwaiter().GetResult().ToArray();
        }

        [Then]
        public void SåSkaShipmentServiceBlivitAnropad()
        {
            ShipmentService.Received(1).GetPreviousShipmentWithAnotherOrderRevisionAsync(ShipmentId);
        }

        [Then]
        public void SåSkaDetFinnasEttResultatIListan()
        {
            Assert.AreEqual(1, _result.Length);
        }

        [Then]
        public void SåSkaDetVaraKorrektItem()
        {
            Assert.AreEqual(1, _result.First().Item.Id);
        }

        [Then]
        public void SåSkaPreviousQuantityVaraNull()
        {
            Assert.IsNull(_result.First().PreviousQuantity);
        }

        [Then]
        public void SåSkaCurrentQuantityVaraKorrekt()
        {
            Assert.AreEqual(4, _result.First().CurrentQuantity);
        }

        [Then]
        public void SåSkaRemovedFromOrderVaraFalse()
        {
            Assert.IsFalse(_result.First().RemovedFromOrder);
        }

        [Then]
        public void SåSkaRemovedFromOrderAndNeedsUnpackingVaraFalse()
        {
            Assert.IsFalse(_result.First().RemovedFromOrderAndNeedsUnpacking);
        }

        [Then]
        public void SåSkaNewVaraTrue()
        {
            Assert.IsTrue(_result.First().New);
        }
    }
}

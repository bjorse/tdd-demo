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
    public class NärEnInteÄrÄndrad : GivetEnChangedOrderItemsLoader
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
                Items = new List<OrderItemShipmentModel>
                {
                    new OrderItemShipmentModel {OrderItem = {Quantity = 4, Item = {Id = 1}}}
                }
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
            _result = Loader.GetChangedOrderItems(_currentModel).GetAwaiter().GetResult().ToArray();
        }

        [Then]
        public void SåSkaShipmentServiceBlivitAnropad()
        {
            ShipmentService.Received(1).GetPreviousShipmentWithAnotherOrderRevisionAsync(ShipmentId);
        }

        [Then]
        public void SåSkaDetInteFinnasNågotResultatIListan()
        {
            Assert.AreEqual(0, _result.Length);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsLoaderTests
{
    public class NärIngenTidigareRevisionFinns : GivetEnChangedOrderItemsLoader
    {
        private const int ShipmentId = 28;

        private OrderShipmentModel _model;

        private ChangedOrderItemResult[] _result;

        protected override void Given()
        {
            base.Given();

            _model = new OrderShipmentModel {Id = ShipmentId};

            ShipmentService.GetPreviousShipmentWithAnotherOrderRevisionAsync(Arg.Any<int>()).Returns(Task.FromResult((OrderShipmentModel) null));
        }

        protected override void When()
        {
            _result = Loader.GetChangedOrderItemsAsync(_model).GetAwaiter().GetResult().ToArray();
        }

        [Then]
        public void SåSkaShipmentServiceBlivitAnropadKorrekt()
        {
            ShipmentService.Received(1).GetPreviousShipmentWithAnotherOrderRevisionAsync(ShipmentId);
        }

        [Then]
        public void SåSkaResultatetVaraTomt()
        {
            Assert.AreEqual(0, _result.Length);
        }
    }
}

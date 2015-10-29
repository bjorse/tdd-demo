using NSubstitute;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsLoaderTests
{
    public abstract class GivetEnChangedOrderItemsLoader : SpecificationBase
    {
        protected IShipmentService ShipmentService { get; private set; }

        protected ChangedOrderItemsLoader Loader { get; private set; }

        protected override void Given()
        {
            ShipmentService = Substitute.For<IShipmentService>();

            Loader = new ChangedOrderItemsLoader(ShipmentService);
        }
    }
}

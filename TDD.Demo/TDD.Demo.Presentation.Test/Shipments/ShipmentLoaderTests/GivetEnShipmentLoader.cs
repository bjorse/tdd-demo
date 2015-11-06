using NSubstitute;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentLoaderTests
{
    public abstract class GivetEnShipmentLoader : SpecificationBase
    {
        protected ICustomerService CustomerServiceMock { get; private set; }

        protected IShipmentService ShipmentServiceMock { get; private set; }

        protected IChangedOrderItemsLoader ChangedOrderItemsLoaderMock { get; private set; }

        protected IChangedOrderItemsInformationLoader ChangedOrderItemsInformationLoaderMock { get; private set; }

        protected ShipmentLoader Loader { get; private set; }

        protected override void Given()
        {
            CustomerServiceMock = Substitute.For<ICustomerService>();
            ShipmentServiceMock = Substitute.For<IShipmentService>();
            ChangedOrderItemsLoaderMock = Substitute.For<IChangedOrderItemsLoader>();
            ChangedOrderItemsInformationLoaderMock = Substitute.For<IChangedOrderItemsInformationLoader>();

            Loader = new ShipmentLoader(CustomerServiceMock, ShipmentServiceMock, ChangedOrderItemsLoaderMock, ChangedOrderItemsInformationLoaderMock);
        }
    }
}

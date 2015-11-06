using NSubstitute;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentViewModelTests
{
    public abstract class GivetEnShipmentViewModel : SpecificationBase
    {
        protected IOrderShipmentViewModelFactory ViewModelFactoryMock { get; private set; }

        protected IShipmentLoader LoaderMock { get; private set; }

        protected ShipmentViewModel ViewModel { get; private set; }

        protected override void Given()
        {
            ViewModelFactoryMock = Substitute.For<IOrderShipmentViewModelFactory>();
            LoaderMock = Substitute.For<IShipmentLoader>();

            ViewModelFactoryMock.CreateOrderShipmentViewModel().Returns(Substitute.For<IOrderShipmentViewModel>());

            ViewModel = new ShipmentViewModel(ViewModelFactoryMock, LoaderMock);
        }
    }
}

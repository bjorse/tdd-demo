using NSubstitute;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentViewModelTests
{
    public abstract class GivetEnShipmentViewModel : SpecificationBase
    {
        protected IOrderShipmentViewModelFactory ViewModelFactory { get; private set; }

        protected IShipmentLoader Loader { get; private set; }

        protected ShipmentViewModel ViewModel { get; private set; }

        protected override void Given()
        {
            ViewModelFactory = Substitute.For<IOrderShipmentViewModelFactory>();
            Loader = Substitute.For<IShipmentLoader>();

            ViewModelFactory.CreateOrderShipmentViewModel(Arg.Any<CustomerModel>(), Arg.Any<OrderShipmentModel>()).Returns(Substitute.For<IOrderShipmentViewModel>());

            ViewModel = new ShipmentViewModel(ViewModelFactory, Loader);
        }
    }
}

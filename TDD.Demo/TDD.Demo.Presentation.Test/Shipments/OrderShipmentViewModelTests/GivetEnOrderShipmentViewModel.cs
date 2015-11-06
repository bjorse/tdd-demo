using System.Threading.Tasks;
using NSubstitute;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.Presentation.Shipments.Savers;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public abstract class GivetEnOrderShipmentViewModel : SpecificationBase
    {
        protected IOrderListItemViewModelFactory OrderListItemViewModelFactoryMock { get; private set; }

        protected IShipmentSaver SaverMock { get; private set; }

        protected OrderShipmentViewModel ViewModel { get; private set; }

        protected override void Given()
        {
            OrderListItemViewModelFactoryMock = Substitute.For<IOrderListItemViewModelFactory>();
            SaverMock = Substitute.For<IShipmentSaver>();

            OrderListItemViewModelFactoryMock.CreateOrderListItem(Arg.Any<OrderItemShipmentModel>()).Returns(x =>
            {
                var viewModel = Substitute.For<IOrderListItemViewModel>();
                viewModel.Model.Returns(x.Arg<OrderItemShipmentModel>());
                return viewModel;
            });
            SaverMock.SaveOrderShipmentAsync(Arg.Any<OrderShipmentModel>()).Returns(Task.FromResult(new OrderShipmentSavedResult()));

            ViewModel = new OrderShipmentViewModel(OrderListItemViewModelFactoryMock, SaverMock);
        }
    }
}

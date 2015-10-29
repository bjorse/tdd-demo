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
        protected IOrderListItemViewModelFactory OrderListItemViewModelFactory { get; private set; }

        protected IShipmentSaver Saver { get; private set; }

        protected OrderShipmentViewModel ViewModel { get; private set; }

        protected override void Given()
        {
            OrderListItemViewModelFactory = Substitute.For<IOrderListItemViewModelFactory>();
            Saver = Substitute.For<IShipmentSaver>();

            OrderListItemViewModelFactory.CreateOrderListItem(Arg.Any<OrderItemShipmentModel>()).Returns(x =>
            {
                var viewModel = Substitute.For<IOrderListItemViewModel>();
                viewModel.Model.Returns(x.Arg<OrderItemShipmentModel>());
                return viewModel;
            });
            Saver.SaveOrderShipmentAsync(Arg.Any<OrderShipmentModel>()).Returns(Task.FromResult(new OrderShipmentSavedResult()));

            ViewModel = new OrderShipmentViewModel(OrderListItemViewModelFactory, Saver);
        }
    }
}

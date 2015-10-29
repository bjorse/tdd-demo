using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments
{
    public interface IOrderListItemViewModelFactory
    {
        IOrderListItemViewModel CreateOrderListItem(OrderItemShipmentModel model);
    }
}

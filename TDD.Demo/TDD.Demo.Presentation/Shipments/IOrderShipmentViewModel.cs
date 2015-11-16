using System.Collections.Generic;
using System.Windows.Input;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments
{
    public interface IOrderShipmentViewModel : IViewModel<OrderShipmentModel>
    {
        void Initialize(CustomerModel customer, OrderShipmentModel orderShipment);

        string Title { get; }

        IList<IOrderListItemViewModel> ItemsToPack { get; }

        IList<IOrderListItemViewModel> PackagedItems { get; }

        string CustomerName { get; }

        string DeliveryAddress { get; }

        decimal TotalPrice { get; }

        ICommand MarkItemAsPackedCommand { get; }

        ICommand SaveCommand { get; }

        ICommand ShipOrderCommand { get; }
    }
}

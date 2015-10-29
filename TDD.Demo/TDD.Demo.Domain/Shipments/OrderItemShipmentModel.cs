using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Domain.Shipments
{
    public class OrderItemShipmentModel : EntityBase
    {
        public OrderItemShipmentModel()
        {
            OrderItem = new OrderItemModel();
        }

        public OrderItemModel OrderItem { get; set; }

        private bool _isPackaged;

        public bool IsPackaged
        {
            get { return _isPackaged; }
            set
            {
                if (Equals(_isPackaged, value))
                {
                    return;
                }

                _isPackaged = value;
                RaisePropertyChanged();
            }
        }
    }
}

using TDD.Demo.Domain.Items;

namespace TDD.Demo.Domain.Orders
{
    public class OrderItemModel : EntityBase
    {
        public OrderItemModel()
        {
            Item = new ItemModel();
        }

        private ItemModel _item;

        public ItemModel Item
        {
            get { return _item; }
            set
            {
                if (Equals(_item, value))
                {
                    return;
                }

                _item = value;
                RaisePropertyChanged();
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (Equals(_quantity, value))
                {
                    return;
                }

                _quantity = value;
                RaisePropertyChanged();
            }
        }
    }
}

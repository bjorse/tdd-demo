namespace TDD.Demo.Domain
{
    public abstract class OrderModelBase : EntityBase
    {
        protected OrderModelBase()
        {
            OrderInfo = new OrderInfoModel();
        }

        private OrderInfoModel _orderInfo;

        public OrderInfoModel OrderInfo
        {
            get { return _orderInfo; }
            set
            {
                if (Equals(_orderInfo, value))
                {
                    return;
                }

                _orderInfo = value;
                RaisePropertyChanged();
            }
        }

        public int OrderNumber { get { return OrderInfo.Id; } }
    }
}

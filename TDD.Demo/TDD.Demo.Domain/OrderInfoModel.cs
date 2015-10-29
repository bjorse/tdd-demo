namespace TDD.Demo.Domain
{
    public class OrderInfoModel : EntityBase
    {
        private int _customerId;

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (Equals(_customerId, value))
                {
                    return;
                }

                _customerId = value;
                RaisePropertyChanged();
            }
        }
    }
}

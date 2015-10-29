namespace TDD.Demo.Domain.Items
{
    public class ItemModel : EntityBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (Equals(_price, value))
                {
                    return;
                }

                _price = value;
                RaisePropertyChanged();
            }
        }
    }
}

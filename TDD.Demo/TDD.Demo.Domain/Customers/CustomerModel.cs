namespace TDD.Demo.Domain.Customers
{
    public class CustomerModel : EntityBase
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Equals(_firstName, value))
                {
                    return;
                }

                _firstName = value;
                RaisePropertyChanged();
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Equals(_lastName, value))
                {
                    return;
                }

                _lastName = value;
                RaisePropertyChanged();
            }
        }

        private string _streetAddress;

        public string StreetAddress
        {
            get { return _streetAddress; }
            set
            {
                if (Equals(_streetAddress, value))
                {
                    return;
                }

                _streetAddress = value;
                RaisePropertyChanged();
            }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                if (Equals(_city, value))
                {
                    return;
                }

                _city = value;
                RaisePropertyChanged();
            }
        }

        private int _zipCode;

        public int ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (Equals(_zipCode, value))
                {
                    return;
                }

                _zipCode = value;
                RaisePropertyChanged();
            }
        }
    }
}

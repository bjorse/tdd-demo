using System;

namespace TDD.Demo.Domain
{
    public class EntityBase : NotificationObject
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value))
                {
                    return;
                }

                _id = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IsPersisted);
            }
        }

        private DateTime _version;

        public DateTime Version
        {
            get { return _version; }
            set
            {
                if (Equals(_version, value))
                {
                    return;
                }

                _version = value;
                RaisePropertyChanged();
            }
        }

        public bool IsPersisted { get { return Id > 0; } }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((EntityBase) obj);
        }

        protected bool Equals(EntityBase other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

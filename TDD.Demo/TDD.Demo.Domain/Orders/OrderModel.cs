using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TDD.Demo.Domain.Orders
{
    public class OrderModel : OrderModelBase
    {
        public OrderModel()
        {
            Items = new ObservableCollection<OrderItemModel>();
        }

        public IList<OrderItemModel> Items { get; set; }

        private DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                if (Equals(_orderDate, value))
                {
                    return;
                }

                _orderDate = value;
                RaisePropertyChanged();
            }
        }

        private bool _readyForDelivery;

        public bool ReadyForDelivery
        {
            get { return _readyForDelivery; }
            set
            {
                if (Equals(_readyForDelivery, value))
                {
                    return;
                }

                _readyForDelivery = value;
                RaisePropertyChanged();
            }
        }
    }
}

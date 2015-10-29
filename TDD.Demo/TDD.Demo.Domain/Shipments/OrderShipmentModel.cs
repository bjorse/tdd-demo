using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TDD.Demo.Domain.Shipments
{
    public class OrderShipmentModel : OrderModelBase
    {
        public OrderShipmentModel()
        {
            Items = new ObservableCollection<OrderItemShipmentModel>();
        }

        public IList<OrderItemShipmentModel> Items { get; set; }

        private int _orderRevision;

        public int OrderRevision
        {
            get { return _orderRevision; }
            set
            {
                if (Equals(_orderRevision, value))
                {
                    return;
                }

                _orderRevision = value;
                RaisePropertyChanged();
            }
        }

        private DateTime? _shippedDate;

        public DateTime? ShippedDate
        {
            get { return _shippedDate; }
            set
            {
                if (Equals(_shippedDate, value))
                {
                    return;
                }

                _shippedDate = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IsShipped);
            }
        }

        public bool IsShipped { get { return ShippedDate != null; } }
    }
}

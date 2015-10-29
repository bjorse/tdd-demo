using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Domain.Shipments
{
    public class OrderShipmentModel : EntityBase
    {
        public OrderShipmentModel()
        {
            OrderInfo = new OrderInfoModel();
            Items = new ObservableCollection<OrderItemShipmentModel>();
        }

        public OrderInfoModel OrderInfo { get; set; }

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

        public int OrderNumber { get { return OrderInfo.Id; } }

        public bool IsShipped { get { return ShippedDate != null; } }
    }
}

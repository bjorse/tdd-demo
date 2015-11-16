using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments.Savers;

namespace TDD.Demo.Presentation.Shipments
{
    public class OrderShipmentViewModel : ViewModelBase<OrderShipmentModel>, IOrderShipmentViewModel
    {
        private readonly IOrderListItemViewModelFactory _orderListItemViewModelFactory;
        private readonly IShipmentSaver _saver;

        private CustomerModel _customer;

        public OrderShipmentViewModel(IOrderListItemViewModelFactory orderListItemViewModelFactory, IShipmentSaver saver)
        {
            _orderListItemViewModelFactory = orderListItemViewModelFactory;
            _saver = saver;

            MarkItemAsPackedCommand = new DelegateCommand(MarkItemAsPackedAction);
            SaveCommand = new DelegateCommand(SaveAction);
            ShipOrderCommand = new DelegateCommand(ShipOrderAction, CanShip);
        }

        public void Initialize(CustomerModel customer, OrderShipmentModel orderShipment, string changedOrderInformation)
        {
            _customer = customer;
            Model = orderShipment;
            WarningMessage = changedOrderInformation;

            ResetItemLists();
            RaisePropertyChanged(() => CustomerName);
            RaisePropertyChanged(() => DeliveryAddress);
            RaisePropertyChanged(() => TotalPrice);
        }

        public string Title
        {
            get { return Model != null ? string.Format("Order #{0} shipment", Model.OrderInfo.Id) : string.Empty; }
        }

        private IList<IOrderListItemViewModel> _itemsToPack = new ObservableCollection<IOrderListItemViewModel>();

        public IList<IOrderListItemViewModel> ItemsToPack
        {
            get { return _itemsToPack; }
            set
            {
                if (Equals(_itemsToPack, value))
                {
                    return;
                }

                _itemsToPack = value;
                RaisePropertyChanged();
            }
        }

        private IList<IOrderListItemViewModel> _packagedItems = new ObservableCollection<IOrderListItemViewModel>();
 
        public IList<IOrderListItemViewModel> PackagedItems
        {
            get { return _packagedItems; }
            set
            {
                if (Equals(_packagedItems, value))
                {
                    return;
                }

                _packagedItems = value;
                RaisePropertyChanged();
            }
        }

        public string CustomerName
        {
            get
            {
                return _customer != null
                    ? string.Format("{0} {1}", _customer.FirstName, _customer.LastName)
                    : string.Empty;
            }
        }

        public string DeliveryAddress
        {
            get
            {
                return _customer != null
                    ? string.Format("{0}, {1}, {2}", _customer.StreetAddress, _customer.ZipCode, _customer.City)
                    : string.Empty;
            }
        }

        public decimal TotalPrice
        {
            get { return Model != null ? Model.Items.Sum(x => x.OrderItem.Quantity*x.OrderItem.Item.Price) : 0m; }
        }

        public ICommand MarkItemAsPackedCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand ShipOrderCommand { get; private set; }

        private bool CanShip()
        {
            return Model != null && Model.Items.All(x => x.IsPackaged);
        }

        private void MarkItemAsPackedAction()
        {
            var selectedItem = ItemsToPack.FirstOrDefault(x => x.IsSelected);

            if (selectedItem == null)
            {
                return;
            }

            selectedItem.Model.IsPackaged = true;

            ResetItemLists();
            RaisePropertyChanged(() => ShipOrderCommand);
        }

        private async void SaveAction()
        {
            await _saver.SaveOrderShipmentAsync(Model);
        }

        private void ShipOrderAction()
        {
            Model.ShippedDate = DateTime.Now;
            SaveAction();
        }

        private void ResetItemLists()
        {
            foreach (var item in ItemsToPack)
            {
                item.OnSelectionChanged -= OnItemsToPackSelectionChanged;
            }

            ItemsToPack = CreateListItems(items => items.Where(x => !x.IsPackaged));
            PackagedItems = CreateListItems(items => items.Where(x => x.IsPackaged));

            foreach (var item in ItemsToPack)
            {
                item.OnSelectionChanged += OnItemsToPackSelectionChanged;
            }
        }

        private void OnItemsToPackSelectionChanged(IOrderListItemViewModel item)
        {
            foreach (var otherItem in ItemsToPack.Where(x => x != item))
            {
                otherItem.SilentDeselect();
            }
        }

        private IList<IOrderListItemViewModel> CreateListItems(Func<IEnumerable<OrderItemShipmentModel>, IEnumerable<OrderItemShipmentModel>> query)
        {
            return Model != null
                ? new ObservableCollection<IOrderListItemViewModel>(query(Model.Items).Select(_orderListItemViewModelFactory.CreateOrderListItem))
                : new ObservableCollection<IOrderListItemViewModel>();
        }
    }
}

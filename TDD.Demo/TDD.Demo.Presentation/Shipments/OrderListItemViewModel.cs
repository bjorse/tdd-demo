using System;
using System.ComponentModel;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments
{
    public class OrderListItemViewModel : ViewModelBase<OrderItemShipmentModel>, IOrderListItemViewModel
    {
        public event Action<IOrderListItemViewModel> OnSelectionChanged;

        public string Title
        {
            get
            {
                return Model != null
                    ? string.Format("{0} of item {1} (#{2})", Model.OrderItem.Quantity, Model.OrderItem.Item.Name, Model.OrderItem.Item.Id)
                    : string.Empty;
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (Equals(_isSelected, value))
                {
                    return;
                }

                _isSelected = value;

                if (OnSelectionChanged != null)
                {
                    OnSelectionChanged(this);
                }
            }
        }

        public void SilentDeselect()
        {
            if (!_isSelected)
            {
                return;
            }

            _isSelected = false;
            RaisePropertyChanged(() => IsSelected);
        }

        protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Model")
            {
                RaisePropertyChanged(() => Title);
            }
        }
    }
}

using System;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments
{
    public interface IOrderListItemViewModel : IViewModel<OrderItemShipmentModel>
    {
        event Action<IOrderListItemViewModel> OnSelectionChanged;

        string Title { get; }

        bool IsSelected { get; set; }

        /// <summary>
        /// Deselects item without raising the <see cref="OnSelectionChanged"/> event
        /// </summary>
        void SilentDeselect();
    }
}

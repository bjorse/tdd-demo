using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TDD.Demo.Presentation.Shipments
{
    public interface IShipmentViewModel : INotifyPropertyChanged
    {
        Task InitializeAsync(int customerId);

        IList<IOrderShipmentViewModel> OrderShipments { get; } 
    }
}

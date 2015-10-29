using System.Threading.Tasks;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public interface IShipmentLoader
    {
        Task<ShipmentLoadResult> LoadAsync(int customerId);
    }
}

using System.Threading.Tasks;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Savers
{
    public interface IShipmentSaver
    {
        Task<OrderShipmentSavedResult> SaveOrderShipmentAsync(OrderShipmentModel orderShipment);
    }
}

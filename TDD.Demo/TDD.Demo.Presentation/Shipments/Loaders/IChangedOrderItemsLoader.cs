using System.Collections.Generic;
using System.Threading.Tasks;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public interface IChangedOrderItemsLoader
    {
        Task<IEnumerable<ChangedOrderItemResult>> GetChangedOrderItemsAsync(OrderShipmentModel shipmentModel);
    }
}

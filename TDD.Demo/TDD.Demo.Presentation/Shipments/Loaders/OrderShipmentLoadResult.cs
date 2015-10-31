using System.Collections.Generic;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class OrderShipmentLoadResult
    {
        public OrderShipmentModel Model { get; set; }

        public IEnumerable<ChangedOrderItemResult> ChangedOrderItems { get; set; }

        public string ChangedOrderInformation { get; set; }
    }
}

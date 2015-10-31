using System.Collections.Generic;
using TDD.Demo.Domain.Customers;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ShipmentLoadResult
    {
        public CustomerModel Customer { get; set; }
        
        public IList<OrderShipmentLoadResult> OrdersToShip { get; set; }
    }
}

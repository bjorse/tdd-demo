using System.Collections.Generic;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ShipmentLoadResult
    {
        public CustomerModel Customer { get; set; }
        
        public IList<OrderShipmentModel> OrdersToShip { get; set; }
    }
}

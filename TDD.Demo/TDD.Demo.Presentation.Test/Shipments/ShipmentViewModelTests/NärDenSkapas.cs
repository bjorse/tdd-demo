using NUnit.Framework;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentViewModelTests
{
    public class NärDenSkapas : GivetEnShipmentViewModel
    {
        protected override void When()
        {
        }

        [Then]
        public void SåSkaOrderShipmentsVaraTom()
        {
            Assert.AreEqual(0, ViewModel.OrderShipments.Count);
        }
    }
}

using NUnit.Framework;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public class NärDenSkapas : GivetEnOrderShipmentViewModel
    {
        protected override void When()
        {
        }

        [Then]
        public void SåSkaMarkItemAsPackedCommandVaraInstansierad()
        {
            Assert.IsNotNull(ViewModel.MarkItemAsPackedCommand);
        }

        [Then]
        public void SåSkaSaveCommandVaraInstansierad()
        {
            Assert.IsNotNull(ViewModel.SaveCommand);
        }

        [Then]
        public void SåSkaShipOrderCommandVaraInstansierad()
        {
            Assert.IsNotNull(ViewModel.ShipOrderCommand);
        }

        [Then]
        public void SåSkaItemsToPackVaraTom()
        {
            Assert.AreEqual(0, ViewModel.ItemsToPack.Count);
        }

        [Then]
        public void SåSkaPackagedItemsVaraTom()
        {
            Assert.AreEqual(0, ViewModel.PackagedItems.Count);
        }

        [Then]
        public void SåSkaCustomerNameVaraEnTomString()
        {
            Assert.AreEqual(string.Empty, ViewModel.CustomerName);
        }

        [Then]
        public void SåSkaDeliveryAddressVaraEnTomString()
        {
            Assert.AreEqual(string.Empty, ViewModel.DeliveryAddress);
        }

        [Then]
        public void SåSkaTotalPriceVaraNoll()
        {
            Assert.AreEqual(0m, ViewModel.TotalPrice);
        }
    }
}

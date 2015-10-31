using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.Presentation.Shipments.Savers;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelFactoryTests
{
    public class NärCreateAnropas : SpecificationBase
    {
        private OrderShipmentViewModelFactory _factory;

        private IOrderShipmentViewModel _createdViewModel;

        protected override void Given()
        {
            _factory = new OrderShipmentViewModelFactory(Substitute.For<IOrderListItemViewModelFactory>(), Substitute.For<IShipmentSaver>());
        }

        protected override void When()
        {
            _createdViewModel = _factory.CreateOrderShipmentViewModel();
        }

        [Then]
        public void SåSkaDetReturneradeObjektetInteVaraNull()
        {
            Assert.IsNotNull(_createdViewModel);
        }

        [Then]
        public void SåÄrDetRättInstans()
        {
            Assert.IsTrue(_createdViewModel.GetType() == typeof(OrderShipmentViewModel));
        }
    }
}

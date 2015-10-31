using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public class NärSaveCommandExekveras : GivetEnOrderShipmentViewModel
    {
        private OrderShipmentModel _model;

        protected override void Given()
        {
            base.Given();

            _model = new OrderShipmentModel();

            ViewModel.Initialize(new CustomerModel(), _model, string.Empty);
        }

        protected override void When()
        {
            ViewModel.SaveCommand.Execute(null);
        }

        [Then]
        public void SåSkaDenGåAttExekvera()
        {
            Assert.IsTrue(ViewModel.SaveCommand.CanExecute(null));
        }

        [Then]
        public void SåSkaShipmentSaverAnropatsKorrekt()
        {
            Saver.Received(1).SaveOrderShipmentAsync(_model);
        }
    }
}

using NUnit.Framework;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderListItemViewModelTests
{
    public class NärIngenModellÄrSatt : SpecificationBase
    {
        private OrderListItemViewModel _viewModel;

        private string _title;

        protected override void Given()
        {
            _viewModel = new OrderListItemViewModel();
        }

        protected override void When()
        {
            _title = _viewModel.Title;
        }

        [Then]
        public void SåSkaTitleVaraEnTomString()
        {
            Assert.AreEqual(string.Empty, _title);
        }
    }
}

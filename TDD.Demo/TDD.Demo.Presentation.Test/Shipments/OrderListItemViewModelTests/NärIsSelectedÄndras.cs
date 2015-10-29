using NUnit.Framework;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderListItemViewModelTests
{
    public class NärIsSelectedÄndras : SpecificationBase
    {
        private OrderListItemViewModel _viewModel;

        private bool _onPropertyChangedWasRaised;
        private bool _onSelectionChangedWasRaised;

        protected override void Given()
        {
            _viewModel = new OrderListItemViewModel();
            _viewModel.PropertyChanged += (sender, args) => _onPropertyChangedWasRaised = _onPropertyChangedWasRaised || args.PropertyName == "IsSelected";
            _viewModel.OnSelectionChanged += model => _onSelectionChangedWasRaised = model == _viewModel;
        }

        protected override void When()
        {
            _viewModel.IsSelected = true;
        }

        [Then]
        public void SåSkaOnSelectionChangedRaisats()
        {
            Assert.IsTrue(_onSelectionChangedWasRaised);
        }

        [Then]
        public void SåSkaInteProperyChangedRaisats()
        {
            Assert.IsFalse(_onPropertyChangedWasRaised);
        }
    }
}

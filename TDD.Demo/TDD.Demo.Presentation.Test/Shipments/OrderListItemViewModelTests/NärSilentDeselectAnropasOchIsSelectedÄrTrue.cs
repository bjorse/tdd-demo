using NUnit.Framework;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderListItemViewModelTests
{
    public class NärSilentDeselectAnropasOchIsSelectedÄrTrue : SpecificationBase
    {
        private OrderListItemViewModel _viewModel;

        private bool _propertyChangedWasRaised;
        private bool _onSelectionChangedWasRaised;

        protected override void Given()
        {
            _viewModel = new OrderListItemViewModel {IsSelected = true};
            _viewModel.PropertyChanged += (sender, args) => _propertyChangedWasRaised = _propertyChangedWasRaised || args.PropertyName == "IsSelected";
            _viewModel.OnSelectionChanged += model => _onSelectionChangedWasRaised = true;
        }

        protected override void When()
        {
            _viewModel.SilentDeselect();
        }

        [Then]
        public void SåSkaInteOnSelectionChangedRaisats()
        {
            Assert.IsFalse(_onSelectionChangedWasRaised);
        }

        [Then]
        public void SåSkaPropertyChangedRaisats()
        {
            Assert.IsTrue(_propertyChangedWasRaised);
        }
    }
}

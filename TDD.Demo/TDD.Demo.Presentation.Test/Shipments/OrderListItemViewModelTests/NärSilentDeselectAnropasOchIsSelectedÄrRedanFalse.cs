using NUnit.Framework;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderListItemViewModelTests
{
    public class NärSilentDeselectAnropasOchIsSelectedÄrRedanFalse : SpecificationBase
    {
        private OrderListItemViewModel _viewModel;

        private bool _propertyWasRaised;
        private bool _onSelectionChangedRaised;

        protected override void Given()
        {
            _viewModel = new OrderListItemViewModel();
            _viewModel.PropertyChanged += (sender, args) => _propertyWasRaised = _propertyWasRaised || args.PropertyName == "IsSelected";
            _viewModel.OnSelectionChanged += model => _onSelectionChangedRaised = true;
        }

        protected override void When()
        {
            _viewModel.SilentDeselect();
        }

        [Then]
        public void SåSkaIsSelectedVaraFalse()
        {
            Assert.IsFalse(_viewModel.IsSelected);
        }

        [Then]
        public void SåSkaIngenProperyChangedRaisats()
        {
            Assert.IsFalse(_propertyWasRaised);
        }

        [Then]
        public void SåSkaIngenOnSelectionChangedRaisats()
        {
            Assert.IsFalse(_onSelectionChangedRaised);
        }
    }
}

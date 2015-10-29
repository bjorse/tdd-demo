using System.Collections.Generic;
using NUnit.Framework;
using TDD.Demo.Domain.Items;
using TDD.Demo.Domain.Orders;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderListItemViewModelTests
{
    public class NärModellSätts : SpecificationBase
    {
        private const string ExpectedTitle = "4 of item Test item (#18)";

        private readonly IList<string> _raisedProperties = new List<string>();
 
        private OrderListItemViewModel _viewModel;
        private OrderItemShipmentModel _model;

        protected override void Given()
        {
            _viewModel = new OrderListItemViewModel();
            _model = new OrderItemShipmentModel
            {
                OrderItem = new OrderItemModel
                {
                    Item = new ItemModel
                    {
                        Id = 18,
                        Name = "Test item"
                    },
                    Quantity = 4
                }
            };
            _viewModel.PropertyChanged += (sender, args) => _raisedProperties.Add(args.PropertyName);
        }

        protected override void When()
        {
            _viewModel.Model = _model;
        }

        [Then]
        public void SåSkaModelVaraSatt()
        {
            Assert.AreEqual(_model, _viewModel.Model);
        }

        [Then]
        public void SåSkaKorrektAntalPropertiesHaRaisats()
        {
            Assert.AreEqual(2, _raisedProperties.Count);
        }

        [Then]
        public void SåSkaTitleVaraKorrekt()
        {
            Assert.AreEqual(ExpectedTitle, _viewModel.Title);
        }
    }
}

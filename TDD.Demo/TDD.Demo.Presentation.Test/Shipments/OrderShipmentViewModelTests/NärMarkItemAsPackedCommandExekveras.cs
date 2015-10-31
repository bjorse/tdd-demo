using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public class NärMarkItemAsPackedCommandExekveras : GivetEnOrderShipmentViewModel
    {
        private readonly List<string> _raisedProperties = new List<string>();

        private OrderItemShipmentModel _model;
        private IOrderListItemViewModel _itemInList;

        protected override void Given()
        {
            base.Given();

            _model = new OrderItemShipmentModel {IsPackaged = false};

            ViewModel.Initialize(new CustomerModel(), new OrderShipmentModel {Items = {_model, new OrderItemShipmentModel {IsPackaged = false}}}, string.Empty);
            ViewModel.PropertyChanged += (sender, args) => _raisedProperties.Add(args.PropertyName);

            _itemInList = ViewModel.ItemsToPack.First();
            _itemInList.Model.Returns(_model);
            _itemInList.IsSelected.Returns(true);
        }

        protected override void When()
        {
            ViewModel.MarkItemAsPackedCommand.Execute(null);
        }

        [Then]
        public void SåSkaDenGåAttExekvera()
        {
            Assert.IsTrue(ViewModel.MarkItemAsPackedCommand.CanExecute(_model));
        }

        [Then]
        public void SåSkaPropertiesHaRaisats()
        {
            Assert.AreEqual(3, _raisedProperties.Count);
        }

        [Then]
        public void SåSkaPropertynItemsToPackRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("ItemsToPack"));
        }

        [Then]
        public void SåSkaPropertynPackagedItemsRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("PackagedItems"));
        }

        [Then]
        public void SåSkaPropertynCanShipRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("CanShip"));
        }

        [Then]
        public void SåSkaIsPackagedVaraTrueIModellen()
        {
            Assert.IsTrue(_model.IsPackaged);
        }

        [Then]
        public void SåSkaDenGamlaVymodellenIListanInteLängreAnvändas()
        {
            var newItem = ViewModel.ItemsToPack.First();
            newItem.OnSelectionChanged += Raise.Event<Action<IOrderListItemViewModel>>(newItem);

            _itemInList.DidNotReceive().SilentDeselect();
        }
    }
}

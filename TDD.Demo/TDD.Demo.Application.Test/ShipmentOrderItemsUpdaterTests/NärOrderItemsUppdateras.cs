using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TDD.Demo.Application.Shipments;
using TDD.Demo.Domain.Orders;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Application.Test.ShipmentOrderItemsUpdaterTests
{
    public class NärOrderItemsUppdateras : SpecificationBase
    {
        private ShipmentOrderItemsUpdater _updater;

        private IList<OrderItemShipmentModel> _currentShipmentOrderItems;
        private IList<OrderItemModel> _updatedOrderItems;

        private IList<OrderItemShipmentModel> _result; 
 
        protected override void Given()
        {
            _updater = new ShipmentOrderItemsUpdater();

            _currentShipmentOrderItems = new List<OrderItemShipmentModel>
            {
                new OrderItemShipmentModel {OrderItem = {Id = 1, Item = {Id = 1}}},
                new OrderItemShipmentModel {OrderItem = {Id = 2, Item = {Id = 2}}},
                new OrderItemShipmentModel {OrderItem = {Id = 3, Item = {Id = 3}}},
                new OrderItemShipmentModel {OrderItem = {Id = 4, Item = {Id = 4}}},
                new OrderItemShipmentModel {OrderItem = {Id = 5, Item = {Id = 5}}}
            };

            _updatedOrderItems = new List<OrderItemModel>
            {
                new OrderItemModel {Id = 6, Item = {Id = 1}},
                new OrderItemModel {Id = 7, Item = {Id = 2}},
                new OrderItemModel {Id = 8, Item = {Id = 3}},
                new OrderItemModel {Id = 9, Item = {Id = 5}},
                new OrderItemModel {Id = 10, Item = {Id = 6}},
                new OrderItemModel {Id = 11, Item = {Id = 7}}
            };
        }

        protected override void When()
        {
            _result = _updater.UpdateOrderItems(_currentShipmentOrderItems, _updatedOrderItems);
        }

        [Then]
        public void SåSkaInteResultatetVaraNull()
        {
            Assert.IsNotNull(_result);
        }

        [Then]
        public void SåSkaDetVaraKorrektAntalElementIResultatet()
        {
            Assert.AreEqual(6, _result.Count);
        }

        [Then]
        public void SåSkaOrderItemFörItem1VaraUppdaterad()
        {
            AssertItemUpdated(1, 6);
        }

        [Then]
        public void SåSkaOrderItemFörItem2VaraUppdaterad()
        {
            AssertItemUpdated(2, 7);
        }

        [Then]
        public void SåSkaOrderItemFörItem3VaraUppdaterad()
        {
            AssertItemUpdated(3, 8);
        }

        [Then]
        public void SåSkaOrderItemFörItem4VaraBorttagen()
        {
            AssertItemDeleted(4);
        }

        [Then]
        public void SåSkaOrderItemFörItem5VaraUppdaterad()
        {
            AssertItemUpdated(5, 9);
        }

        [Then]
        public void SåSkaOrderItemFörItem6VaraUppdaterad()
        {
            AssertItemUpdated(6, 10);
        }

        [Then]
        public void SåSkaOrderItemFörItem7VaraUppdaterad()
        {
            AssertItemUpdated(7, 11);
        }

        private void AssertItemUpdated(int itemId, int expectedShipmentOrderId)
        {
            var orderItem = _result.FirstOrDefault(x => x.OrderItem.Item.Id == itemId);

            Assert.IsNotNull(orderItem);
            Assert.AreEqual(expectedShipmentOrderId, orderItem.OrderItem.Id);
        }

        private void AssertItemDeleted(int itemId)
        {
            var orderItem = _result.FirstOrDefault(x => x.OrderItem.Item.Id == itemId);

            Assert.IsNull(orderItem);
        }
    }
}

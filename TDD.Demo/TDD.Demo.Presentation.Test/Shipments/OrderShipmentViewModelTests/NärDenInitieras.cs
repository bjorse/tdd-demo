using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TDD.Demo.Domain.Customers;
using TDD.Demo.Domain.Shipments;
using TDD.Demo.Presentation.Shipments;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.OrderShipmentViewModelTests
{
    public class NärDenInitieras : GivetEnOrderShipmentViewModel
    {
        private const int OrderNumber = 72;
        private const string ExpectedTitle = "Order #72 shipment";
        private const int ExpectedItemsToPackCount = 3;
        private const int ExpectedPackedItemsCount = 4;
        private const string ExpectedCustomerName = "John Doe";
        private const string ExpectedDeliveryAddress = "Delivery street 15, 12345, Delivery town";
        private const decimal ExpectedTotalPrice = 671.5m;
        private const string ExpectedWarningMessage = "Detta är information om att ordern har ändrats";

        private readonly IList<string> _raisedProperties = new List<string>();

        private CustomerModel _customer;
        private OrderShipmentModel _orderShipment;
        private OrderItemShipmentModel[] _allOrderItems;

        private IList<IOrderListItemViewModel> _itemsToPack;
        private IList<IOrderListItemViewModel> _packagedItems; 

        protected override void Given()
        {
            base.Given();

            ViewModel.PropertyChanged += (sender, args) => _raisedProperties.Add(args.PropertyName);

            _customer = new CustomerModel
            {
                City = "Delivery town",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "Delivery street 15",
                ZipCode = 12345
            };

            _allOrderItems = new[]
            {
                new OrderItemShipmentModel
                {
                    Id = 1,
                    IsPackaged = false,
                    OrderItem =
                    {
                        Quantity = 3,
                        Item = {Price = 18m}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 2,
                    IsPackaged = true,
                    OrderItem =
                    {
                        Quantity = 1,
                        Item = {Price = 1.5m}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 3,
                    IsPackaged = false,
                    OrderItem = 
                    {
                        Quantity = 4,
                        Item = {Price = 100m}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 4,
                    IsPackaged = true,
                    OrderItem = 
                    {
                        Quantity = 1,
                        Item = {Price = 10m}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 5,
                    IsPackaged = true,
                    OrderItem = 
                    {
                        Quantity = 10,
                        Item = {Price = 10m}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 6,
                    IsPackaged = true,
                    OrderItem =
                    {
                        Quantity = 3,
                        Item = {Price = 25}
                    }
                },
                new OrderItemShipmentModel
                {
                    Id = 7,
                    IsPackaged = false,
                    OrderItem =
                    {
                        Quantity = 2,
                        Item = {Price = 15.5m}
                    }
                }
            };

            _orderShipment = new OrderShipmentModel {Items = _allOrderItems, OrderInfo = {Id = OrderNumber}};
        }

        protected override void When()
        {
            ViewModel.Initialize(_customer, _orderShipment, ExpectedWarningMessage);

            _itemsToPack = ViewModel.ItemsToPack;
            _packagedItems = ViewModel.PackagedItems;
        }

        [Then]
        public void SåSkaTitleVaraKorrekt()
        {
            Assert.AreEqual(ExpectedTitle, ViewModel.Title);
        }

        [Then]
        public void SåSkaOrderListItemViewModelFactoryBlivitAnropadKorrektAntalGånger()
        {
            OrderListItemViewModelFactory.Received(7).CreateOrderListItem(Arg.Any<OrderItemShipmentModel>());
        }

        [Then]
        public void SåSkaOrderListItemViewModelFactoryBlivitAnropadMedKorrektModeller()
        {
            foreach (var orderItem in _allOrderItems)
            {
                OrderListItemViewModelFactory.Received(1).CreateOrderListItem(orderItem);
            }
        }

        [Then]
        public void SåSkaDetVaraKorrektAntalItemsToPack()
        {
            Assert.AreEqual(ExpectedItemsToPackCount, _itemsToPack.Count);
        }

        [Then]
        public void SåSkaDetVaraKorrektElementIItemsToPack()
        {
            Assert.IsTrue(_allOrderItems.Where(x => !x.IsPackaged).All(x => _itemsToPack.Select(y => y.Model).Contains(x)));
        }

        [Then]
        public void SåSkaItemsToPackVaraObservableCollection()
        {
            Assert.IsTrue(_itemsToPack.GetType() == typeof(ObservableCollection<IOrderListItemViewModel>));
        }

        [Then]
        public void SåSkaDetVaraKorrektAntalPackagedItems()
        {
            Assert.AreEqual(ExpectedPackedItemsCount, _packagedItems.Count);
        }

        [Then]
        public void SåSkaDetVaraKorrektElementIPackagedItems()
        {
            Assert.IsTrue(_allOrderItems.Where(x => x.IsPackaged).All(x => _packagedItems.Select(y => y.Model).Contains(x)));
        }

        [Then]
        public void SåSkaPackagedItemsVaraObservableCollection()
        {
            Assert.IsTrue(_packagedItems.GetType() == typeof(ObservableCollection<IOrderListItemViewModel>));
        }

        [Then]
        public void SåSkaCustomerNameVaraKorrekt()
        {
            Assert.AreEqual(ExpectedCustomerName, ViewModel.CustomerName);
        }

        [Then]
        public void SåSkaDeliveryAddressVaraKorrekt()
        {
            Assert.AreEqual(ExpectedDeliveryAddress, ViewModel.DeliveryAddress);
        }

        [Then]
        public void SåSkaTotalPriceVaraKorrekt()
        {
            Assert.AreEqual(ExpectedTotalPrice, ViewModel.TotalPrice);
        }

        [Then]
        public void SåSkaWarningMessageVaraKorrekt()
        {
            Assert.AreEqual(ExpectedWarningMessage, ViewModel.WarningMessage);
        }

        [Then]
        public void SåSkaKorrektAntalPropertiesHaRaisats()
        {
            Assert.AreEqual(7, _raisedProperties.Count);
        }

        [Then]
        public void SåSkaPropertynModelRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("Model"));
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
        public void SåSkaPropertynCustomerNameRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("CustomerName"));
        }

        [Then]
        public void SåSkaPropertynDeliveryAddressRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("DeliveryAddress"));
        }

        [Then]
        public void SåSkaPropertynTotalPriceRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("TotalPrice"));
        }

        [Then]
        public void SåSkaPropertynWarningMessageRaisats()
        {
            Assert.IsTrue(_raisedProperties.Contains("WarningMessage"));
        }
    }
}

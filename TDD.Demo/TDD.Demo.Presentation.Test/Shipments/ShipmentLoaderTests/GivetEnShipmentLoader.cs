﻿using NSubstitute;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ShipmentLoaderTests
{
    public abstract class GivetEnShipmentLoader : SpecificationBase
    {
        protected ICustomerService CustomerService { get; private set; }

        protected IShipmentService ShipmentService { get; private set; }

        protected IChangedOrderItemsLoader ChangedOrderItemsLoader { get; private set; }

        protected IChangedOrderItemsInformationLoader ChangedOrderItemsInformationLoader { get; private set; }

        protected ShipmentLoader Loader { get; private set; }

        protected override void Given()
        {
            CustomerService = Substitute.For<ICustomerService>();
            ShipmentService = Substitute.For<IShipmentService>();
            ChangedOrderItemsLoader = Substitute.For<IChangedOrderItemsLoader>();
            ChangedOrderItemsInformationLoader = Substitute.For<IChangedOrderItemsInformationLoader>();

            Loader = new ShipmentLoader(CustomerService, ShipmentService, ChangedOrderItemsLoader, ChangedOrderItemsInformationLoader);
        }
    }
}

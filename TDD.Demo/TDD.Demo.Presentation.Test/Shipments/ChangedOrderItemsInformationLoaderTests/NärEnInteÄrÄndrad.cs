using System;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public class NärEnInteÄrÄndrad : GivetEnChangedOrderItemsInformationLoader
    {
        private ChangedOrderItemResult[] _notChangedOrderItem;

        protected override void Given()
        {
            base.Given();

            _notChangedOrderItem = new[]
            {
                new ChangedOrderItemResult
                {
                    CurrentQuantity = 1,
                    Item = {Id = 1},
                    New = false,
                    PreviousQuantity = 1,
                    RemovedFromOrder = false,
                    RemovedFromOrderAndNeedsUnpacking = false
                }
            };
        }

        protected override void When()
        {
        }

        [Then]
        public void SåSkaEttExceptionKastas()
        {
            Assert.Throws<ArgumentException>(() => Loader.GetChangedOrderItemInformation(_notChangedOrderItem));
        }
    }
}

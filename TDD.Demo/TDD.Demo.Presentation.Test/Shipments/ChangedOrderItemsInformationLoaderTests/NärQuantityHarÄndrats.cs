using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public class NärQuantityHarÄndrats : GivetEnChangedOrderItemsInformationLoader
    {
        private const string ExpectedChangedText = "The quantity of Testy test (#82) has changed from 10 to 4";

        private IEnumerable<ChangedOrderItemResult> _changedOrderItems;

        private string[] _result;

        protected override void Given()
        {
            base.Given();

            _changedOrderItems = new[]
            {
                new ChangedOrderItemResult
                {
                    CurrentQuantity = 4,
                    Item = {Id = 82, Name = "Testy test"},
                    New = false,
                    PreviousQuantity = 10,
                    RemovedFromOrder = false,
                    RemovedFromOrderAndNeedsUnpacking = false
                }
            };
        }

        protected override void When()
        {
            _result = ParseResult(Loader.GetChangedOrderItemInformation(_changedOrderItems));
        }

        [Then]
        public void SåSkaDetVaraKorrektAntalRader()
        {
            Assert.AreEqual(2, _result.Length);
        }

        [Then]
        public void SåSkaTitelnVaraKorrekt()
        {
            Assert.AreEqual(Header, _result.First());
        }

        [Then]
        public void SåSkaDetFinnasInformationOmAttEttItemÄrBorttaget()
        {
            Assert.AreEqual(ExpectedChangedText, _result.Skip(1).First());
        }
    }
}

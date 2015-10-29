using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public class NärEnÄrBorttagenSomÄrPaketerad : GivetEnChangedOrderItemsInformationLoader
    {
        private const string ExpectedChangedText = "The Testy test (#72) has been packaged but is no longer in the order";

        private IEnumerable<ChangedOrderItemResult> _changedOrderItems;

        private string[] _result;

        protected override void Given()
        {
            base.Given();

            _changedOrderItems = new[]
            {
                new ChangedOrderItemResult
                {
                    CurrentQuantity = 0,
                    Item = {Id = 72, Name = "Testy test"},
                    New = false,
                    PreviousQuantity = 0,
                    RemovedFromOrder = true,
                    RemovedFromOrderAndNeedsUnpacking = true
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

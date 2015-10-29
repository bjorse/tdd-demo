using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public class NärEnÄrNy : GivetEnChangedOrderItemsInformationLoader
    {
        private const string ExpectedChangedText = "A new order of 4 Testy test (#82) has been added";

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
                    New = true,
                    PreviousQuantity = null,
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

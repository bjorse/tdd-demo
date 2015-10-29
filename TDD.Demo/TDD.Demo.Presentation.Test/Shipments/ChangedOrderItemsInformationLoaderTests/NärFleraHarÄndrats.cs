﻿using System.Linq;
using NUnit.Framework;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public class NärFleraHarÄndrats : GivetEnChangedOrderItemsInformationLoader
    {
        private readonly string[] _expectedResult =
        {
            Header,
            "The quantity of First item (#1) has changed from 3 to 2",
            "The quantity of Third item (#3) has changed from 10 to 8",
            "A new order of 2 Nineth item (#9) has been added",
            "A new order of 8 Eigth item (#8) has been added",
            "The Fifth item (#5) should no longer be packaged",
            "The Fourth item (#4) should no longer be packaged",
            "The Sixth item (#6) has been packaged but is no longer in the order"
        };

        private ChangedOrderItemResult[] _changedOrderItems;

        private string[] _result;

        protected override void Given()
        {
            base.Given();

            _changedOrderItems = new[]
            {
                new ChangedOrderItemResult
                {
                    Item = {Id = 1, Name = "First item"},
                    CurrentQuantity = 2,
                    New = false,
                    PreviousQuantity = 3,
                    RemovedFromOrder = false,
                    RemovedFromOrderAndNeedsUnpacking = false
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 3, Name = "Third item"},
                    CurrentQuantity = 8,
                    New = false,
                    PreviousQuantity = 10,
                    RemovedFromOrder = false,
                    RemovedFromOrderAndNeedsUnpacking = false
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 4, Name = "Fourth item"},
                    CurrentQuantity = 1,
                    New = false,
                    PreviousQuantity = 1,
                    RemovedFromOrder = true,
                    RemovedFromOrderAndNeedsUnpacking = false
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 5, Name = "Fifth item"},
                    CurrentQuantity = 1,
                    New = false,
                    PreviousQuantity = 1,
                    RemovedFromOrder = true,
                    RemovedFromOrderAndNeedsUnpacking = false
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 6, Name = "Sixth item"},
                    CurrentQuantity = 1,
                    New = false,
                    PreviousQuantity = 1,
                    RemovedFromOrder = true,
                    RemovedFromOrderAndNeedsUnpacking = true
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 8, Name = "Eigth item"},
                    CurrentQuantity = 8,
                    New = true,
                    PreviousQuantity = null,
                    RemovedFromOrder = false,
                    RemovedFromOrderAndNeedsUnpacking = false
                },
                new ChangedOrderItemResult
                {
                    Item = {Id = 9, Name = "Nineth item"},
                    CurrentQuantity = 2,
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
            Assert.AreEqual(_expectedResult.Length, _result.Length);
        }

        [Then]
        public void SåSkaAllaRaderVaraKorrektOchIRättOrdning()
        {
            Assert.IsTrue(_expectedResult.SequenceEqual(_result));
        }
    }
}

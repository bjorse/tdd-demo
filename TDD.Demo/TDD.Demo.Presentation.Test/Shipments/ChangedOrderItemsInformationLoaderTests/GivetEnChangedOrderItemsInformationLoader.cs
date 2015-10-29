using System;
using TDD.Demo.Presentation.Shipments.Loaders;
using TDD.Demo.TestTools;

namespace TDD.Demo.Presentation.Test.Shipments.ChangedOrderItemsInformationLoaderTests
{
    public abstract class GivetEnChangedOrderItemsInformationLoader : SpecificationBase
    {
        protected const string Header = "The following items in the order has changed:";

        protected ChangedOrderItemsInformationLoader Loader { get; private set; }

        protected override void Given()
        {
            Loader = new ChangedOrderItemsInformationLoader();
        }

        protected string[] ParseResult(string result)
        {
            return result.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

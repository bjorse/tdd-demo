using System.Collections.Generic;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public interface IChangedOrderItemsInformationLoader
    {
        string GetChangedOrderItemInformation(IEnumerable<ChangedOrderItemResult> changedOrderItems);
    }
}

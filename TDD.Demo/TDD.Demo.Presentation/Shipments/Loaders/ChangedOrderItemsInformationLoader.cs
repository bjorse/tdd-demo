using System;
using System.Collections.Generic;
using System.Linq;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ChangedOrderItemsInformationLoader : IChangedOrderItemsInformationLoader
    {
        private const string Header = "The following items in the order has changed:";

        public string GetChangedOrderItemInformation(IEnumerable<ChangedOrderItemResult> changedOrderItems)
        {
            var result = new List<string>();

            foreach (var changedOrderItem in changedOrderItems)
            {
                if (changedOrderItem.RemovedFromOrderAndNeedsUnpacking)
                {
                    result.Add(string.Format("The {0} (#{1}) has been packaged but is no longer in the order", changedOrderItem.Item.Name, changedOrderItem.Item.Id));
                    continue;
                }

                if (changedOrderItem.RemovedFromOrder)
                {
                    result.Add(string.Format("The {0} (#{1}) should no longer be packaged", changedOrderItem.Item.Name, changedOrderItem.Item.Id));
                    continue;
                }

                if (changedOrderItem.New)
                {
                    result.Add(string.Format("A new order of {0} {1} (#{2}) has been added", changedOrderItem.CurrentQuantity, changedOrderItem.Item.Name, changedOrderItem.Item.Id));
                    continue;
                }

                if (changedOrderItem.PreviousQuantity != changedOrderItem.CurrentQuantity)
                {
                    result.Add(string.Format("The quantity of {0} (#{1}) has changed from {2} to {3}", changedOrderItem.Item.Name, changedOrderItem.Item.Id, changedOrderItem.PreviousQuantity, changedOrderItem.CurrentQuantity));
                }
            }

            if (result.Any())
            {
                result.Insert(0, Header);
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}

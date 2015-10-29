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
            var result = GetChangedOrderInformation(changedOrderItems).ToArray();

            return result.Any()
                ? string.Join(Environment.NewLine, new[] {Header}.Concat(result.OrderBy(x => x.Item1).ThenBy(x => x.Item2).Select(x => x.Item2)))
                : string.Empty;
        }

        private static IEnumerable<Tuple<int, string>>  GetChangedOrderInformation(IEnumerable<ChangedOrderItemResult> changedItems)
        {
            return changedItems.Select(x =>
            {
                if (x.RemovedFromOrderAndNeedsUnpacking)
                {
                    return new Tuple<int, string>(4, string.Format("The {0} (#{1}) has been packaged but is no longer in the order", x.Item.Name, x.Item.Id));
                }

                if (x.RemovedFromOrder)
                {
                    return new Tuple<int, string>(3, string.Format("The {0} (#{1}) should no longer be packaged", x.Item.Name, x.Item.Id));
                }

                if (x.New)
                {
                    return new Tuple<int, string>(2, string.Format("A new order of {0} {1} (#{2}) has been added", x.CurrentQuantity, x.Item.Name, x.Item.Id));
                }

                if (x.PreviousQuantity != x.CurrentQuantity)
                {
                    return new Tuple<int, string>(1, string.Format("The quantity of {0} (#{1}) has changed from {2} to {3}", x.Item.Name, x.Item.Id, x.PreviousQuantity, x.CurrentQuantity));
                }

                throw new ArgumentException(string.Format("The item #{0} has not been changed!", x.Item.Id));
            });
        }
    }
}

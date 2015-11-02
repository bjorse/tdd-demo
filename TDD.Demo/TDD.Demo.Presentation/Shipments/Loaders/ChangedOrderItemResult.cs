using TDD.Demo.Domain.Items;

namespace TDD.Demo.Presentation.Shipments.Loaders
{
    public class ChangedOrderItemResult
    {
        public ChangedOrderItemResult()
        {
            Item = new ItemModel();
        }

        public ItemModel Item { get; set; }

        public int? PreviousQuantity { get; set; }

        public int? CurrentQuantity { get; set; }

        public bool RemovedFromOrder { get; set; }

        public bool RemovedFromOrderAndNeedsUnpacking { get; set; }

        public bool New { get; set; }
    }
}

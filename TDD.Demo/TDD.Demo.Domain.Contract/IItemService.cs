using System.Collections.Generic;
using System.Threading.Tasks;
using TDD.Demo.Domain.Items;

namespace TDD.Demo.Domain.Contract
{
    public interface IItemService
    {
        Task<IEnumerable<ItemModel>> GetAllItemsAsync();

        Task<ItemModel> GetItemByIdAsync(int itemId);

        Task<int> GetItemsInStockAsync(int itemId);
    }
}

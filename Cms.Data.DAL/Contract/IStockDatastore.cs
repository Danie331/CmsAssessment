
using Cms.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Data.DAL.Contract
{
    public interface IStockDatastore
    {
        Task AddAsync(StockItem stockItem);
        Task DeleteAsync(int stockItemId);
        Task<IEnumerable<StockItem>> FindAsync(StockItem searchItem, PaginationFilter filter = null);
        Task<IEnumerable<StockItem>> GetAsync(PaginationFilter filter = null);
        Task<StockItem> GetAsync(int id);
        Task UpdateAsync(StockItem stockItem);
        Task AddAsync(StockAccessory stockAccessory);
        Task AddAsync(Image image);
        Task DeleteAccessoryAsync(int stockItemAccessoryId);
        Task DeleteImageAsync(int stockItemImageId);
    }
}


using Cms.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Contract.StockManagementService
{
    public interface IStockQueryService
    {
        Task<StockItem> GetStockItemAsync(int id);
        Task<IEnumerable<StockItem>> GetStockItemsAsync(PaginationFilter pagination = null);
        Task<IEnumerable<StockItem>> SearchStockAsync(StockItem searchItem, PaginationFilter pagination = null);
    }
}

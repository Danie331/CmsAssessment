using Cms.Data.DAL.Contract;
using Cms.Services.Contract.StockManagementService;
using Cms.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Core.StockManagementService
{
    class StockQueryService : IStockQueryService
    {
        private readonly IStockDatastore _store;
        public StockQueryService(IStockDatastore store)
        {
            _store = store;
        }

        public Task<StockItem> GetStockItemAsync(int id)
        {
            return _store.GetAsync(id);
        }

        public Task<IEnumerable<StockItem>> GetStockItemsAsync(PaginationFilter pagination = null)
        {
            return _store.GetAsync(pagination);
        }

        public Task<IEnumerable<StockItem>> SearchStockAsync(StockItem searchItem, PaginationFilter pagination = null)
        {
            return _store.FindAsync(searchItem, pagination);
        }
    }
}

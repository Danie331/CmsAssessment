using Cms.Data.DAL.Contract;
using Cms.Services.Contract.StockManagementService;
using Cms.Types;
using System.Threading.Tasks;

namespace Cms.Services.Core.StockManagementService
{
    class StockUpdateService : IStockUpdateService
    {
        private readonly IStockDatastore _store;
        public StockUpdateService(IStockDatastore store)
        {
            _store = store;
        }

        public Task AddAsync(StockItem item)
        {
            return _store.AddAsync(item);
        }

        public Task AddAsync(Image item)
        {
            return _store.AddAsync(item);
        }

        public Task AddAsync(StockAccessory item)
        {
            return _store.AddAsync(item);
        }

        public Task UpdateAsync(StockItem item)
        {
            return _store.UpdateAsync(item);
        }

        public Task DeleteAsync(int stockItemId)
        {
            return _store.DeleteAsync(stockItemId);
        }

        public Task DeleteAccessoryAsync(int accessoryId)
        {
            return _store.DeleteAccessoryAsync(accessoryId);
        }

        public Task DeleteImageAsync(int imageId)
        {
            return _store.DeleteImageAsync(imageId);
        }
    }
}

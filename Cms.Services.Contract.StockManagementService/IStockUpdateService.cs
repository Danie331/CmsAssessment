
using Cms.Types;
using System.Threading.Tasks;

namespace Cms.Services.Contract.StockManagementService
{
    public interface IStockUpdateService
    {
        Task AddAsync(StockItem item);
        Task AddAsync(Image item);
        Task AddAsync(StockAccessory item);
        Task DeleteAsync(int stockItemId);
        Task DeleteAccessoryAsync(int accessoryId);
        Task DeleteImageAsync(int imageId);
        Task UpdateAsync(StockItem item);
    }
}

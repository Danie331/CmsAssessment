
namespace Cms.StockManagementApi.Models
{
    public class StockAccessory
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

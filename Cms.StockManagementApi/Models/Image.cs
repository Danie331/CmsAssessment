
namespace Cms.StockManagementApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}


namespace Cms.Types
{
    public class Image
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
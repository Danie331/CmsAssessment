
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Data.DAL.Context.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

        [ForeignKey("StockItemId")]
        public StockItem StockItem { get; set; }
    }
}

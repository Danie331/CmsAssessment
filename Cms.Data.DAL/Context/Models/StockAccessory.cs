
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Data.DAL.Context.Models
{
    public class StockAccessory
    {
        public int Id { get; set; }
        [Required]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("StockItemId")]
        public StockItem StockItem { get; set; }
    }
}

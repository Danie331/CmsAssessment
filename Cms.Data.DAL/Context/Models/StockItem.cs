using System;
using System.Collections.Generic;

namespace Cms.Data.DAL.Context.Models
{
    public class StockItem
    {
        public StockItem()
        {
            Image = new HashSet<Image>();
            StockAccessory = new HashSet<StockAccessory>();
        }

        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public int Kms { get; set; }
        public string Colour { get; set; }
        public string Vin { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime Dtcreated { get; set; }
        public DateTime? Dtupdated { get; set; }

        public ICollection<Image> Image { get; set; }
        public ICollection<StockAccessory> StockAccessory { get; set; }
    }
}

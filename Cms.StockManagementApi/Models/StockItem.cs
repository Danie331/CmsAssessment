
using System;
using System.Collections.Generic;

namespace Cms.StockManagementApi.Models
{
    public class StockItem
    {
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

        public IEnumerable<Image> Image { get; set; }
        public IEnumerable<StockAccessory> StockAccessory { get; set; }
    }
}

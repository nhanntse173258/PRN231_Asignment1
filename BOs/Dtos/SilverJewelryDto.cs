using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Dtos
{
    public class SilverJewelryDto
    {
        public string SilverJewelryId { get; set; }
        public string SilverJewelryName { get; set; }
        public string SilverJewelryDescription { get; set; }
        public decimal? MetalWeight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ProductionYear { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } // New field for category name
        public SilverJewelryDto()
        {
            
        }
        public SilverJewelryDto(string silverJewelryId, string silverJewelryName, string silverJewelryDescription, decimal? metalWeight, DateTime? createdDate, int productionYear, decimal price, string categoryName)
        {
            SilverJewelryId = silverJewelryId;
            SilverJewelryName = silverJewelryName;
            SilverJewelryDescription = silverJewelryDescription;
            MetalWeight = metalWeight;
            CreatedDate = createdDate;
            ProductionYear = productionYear;
            Price = price;
            CategoryName = categoryName;
        }
    }
}

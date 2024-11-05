using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace BOs.Entities
{
    public partial class SilverJewelry
    {
        public string SilverJewelryId { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9]*( [A-Z][a-zA-Z0-9]*)*$", ErrorMessage = "Each word must start with a capital letter and include only letters and digits.")]
        public string SilverJewelryName { get; set; } = null!;
        [Required]
        public string? SilverJewelryDescription { get; set; }
        [Required]
        public decimal? MetalWeight { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal? Price { get; set; }
        [Required]
        [Range(1900, int.MaxValue, ErrorMessage = "Production Year must be greater than or equal to 1900.")]
        public int? ProductionYear { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}


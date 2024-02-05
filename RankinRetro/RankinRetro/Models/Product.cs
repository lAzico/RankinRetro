

using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace RankinRetro.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public Size Size { get; set; }
        public Colour Colour { get; set; }
        public Material Material { get; set; }
        public int QuantityPurchased { get; set; }
        public string ImageURL { get; set; }

    }
}

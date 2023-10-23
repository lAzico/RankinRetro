

using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace RankinRetro.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int brandId { get; set; }
        public int categoryId { get; set; }
        public Size Size { get; set; }
        public Colour Colour { get; set; }
        public Material Material { get; set; }
        public string ImageURL { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace RankinRetro.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Website { get; set;}
    }
}

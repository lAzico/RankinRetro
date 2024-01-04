using System.ComponentModel.DataAnnotations;

namespace RankinRetro.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required]
        public string DiscountName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }
        
    }
}

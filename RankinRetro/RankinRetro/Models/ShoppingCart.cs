using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartItemId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId {  get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}

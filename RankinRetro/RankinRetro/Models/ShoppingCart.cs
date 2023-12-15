using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class ShoppingCart
    {
        [Key]

        public int ShoppingCartId { get; set; }
        public int CartItemId { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerId {  get; set; }

        public ICollection<ShoppingCartDetail> Details { get; set; }
        



    }
}

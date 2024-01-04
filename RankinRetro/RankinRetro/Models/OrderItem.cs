using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        [ForeignKey("ProductId")]
        public string Name { get; set; }
        public string URL {  get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountAmount {  get; set; }
    }
}

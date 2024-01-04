using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RankinRetro.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public DateTime OrderDate {  get; set; }
        public Status Status { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountAmount {  get; set; }
        public ICollection<OrderItem> Details { get; set; }
    }
}

using RankinRetro.Models;

namespace RankinRetro.ViewModels
{
    public class OrderDisplayViewModel
    {
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<OrderAddress> Address { get; set; }
        public List<OrderBillingAddress> BillingAddress { get; set; }
    }
}

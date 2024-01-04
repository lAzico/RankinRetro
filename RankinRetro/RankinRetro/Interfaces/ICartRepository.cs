using Microsoft.EntityFrameworkCore;
using RankinRetro.Migrations;
using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productID, int qty);

        Task<int> RemoveItem(int productID);

        Task<ShoppingCart> GetUserCart();

        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<Order> Checkout(decimal discountAmount);

        Task<int> AddOneItem(int productID);
        
        bool AddBillingAddress(OrderBillingAddress billingAddress);
        bool AddOrderAddress(OrderAddress orderAddress);
        decimal GetDiscountAmount(string discountCode);

        bool Save();

    }
}

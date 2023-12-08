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
        Task<bool> Checkout();

    }
}

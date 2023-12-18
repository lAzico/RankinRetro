using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUserOrdersAsync();

        Task<List<OrderItem>> GetOrderItemsAsync(Guid orderId);
        Task<Order> RemoveItem(int productId);
        string GetUserID();

    }
}

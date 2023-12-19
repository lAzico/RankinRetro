using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUserOrdersAsync();

        Task<List<OrderItem>> GetOrderItemsAsync(Guid orderId);
        Task<OrderItem> RemoveItem(Guid orderId, int productId);
        Task<Order> RemoveOrder(Guid order);

        string GetUserID();

    }
}

using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUserOrdersAsync();

        Task<List<OrderItem>> GetOrderItemsAsync(Guid orderId);
        Task<OrderItem> RemoveItem(Guid orderId, int productId);
        Task<Order> RemoveOrder(Guid order);
        Task<List<OrderAddress>> GetOrderAddressAsync(string customerId);
        Task<List<OrderBillingAddress>> GetBillingAddressAsync(string customerId);

        string GetUserID();

    }
}

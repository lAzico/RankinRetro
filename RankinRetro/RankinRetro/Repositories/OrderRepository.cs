using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Customer> _userManager;

        public OrderRepository(ApplicationDbContext context, HttpContextAccessor httpContextAccessor, UserManager<Customer> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<List<Order>> GetUserOrdersAsync()
        {
            string userId = GetUserID();
            var Orders = await _context.Orders.Where(order => order.CustomerId == userId).ToListAsync();

            return Orders;
        }

        public async Task<List<OrderItem>> GetOrderItemsAsync(Guid orderId)
        {
            var orderItems = await _context.OrderItems.Where(order => order.OrderId == orderId).ToListAsync();
            return orderItems;
        }


            public async Task<Order> RemoveItem(int productId)
        {
            throw new NotImplementedException();
        }

        public string GetUserID()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);
            return userId;
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


            public async Task<OrderItem> RemoveItem(Guid orderId, int productId)
        {

            var orderItems = await GetOrderItemsAsync(orderId);
            var product = orderItems.Where(x => x.ProductId == productId).FirstOrDefault();

            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
            }
            return product;
        }


        public async Task<Order> RemoveOrder(Guid orderId)
        {
            var userID = GetUserID();
            var order = GetUserOrdersAsync().Result.FirstOrDefault(x => x.OrderId == orderId);


            if (userID == order.CustomerId)
            {
                //Only remove order if the status is pending and
                if (order.Status == Data.Enum.Status.Pending)
                {
                    _context.Remove(order);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot cancel successful order");
                }
            }
            return order;
        }



        public string GetUserID()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);
            return userId;
        }

    }
}

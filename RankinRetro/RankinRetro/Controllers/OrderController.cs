using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Customer> _userManager;

        public OrderController(IOrderRepository orderRepository, HttpContextAccessor httpContextAccessor, UserManager<Customer> userManager)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }


        public IActionResult Details()
        { 
            var orders = _orderRepository.GetUserOrdersAsync().Result;
            List<OrderItem> ordersItems = new List<OrderItem>();
            foreach (var order in orders)
            {
                var orderItemTask = _orderRepository.GetOrderItemsAsync(order.OrderId).Result;
                foreach (var item in orderItemTask)
                {
                    var orderItem = orderItemTask.FirstOrDefault();
                    ordersItems.Add(orderItem);
                }
                
            }
            
            OrderDisplayViewModel orderVM = new OrderDisplayViewModel()
            {
                Orders = orders,
                OrderItems = ordersItems
            };

            return View(orderVM);
            
        }
    }
}

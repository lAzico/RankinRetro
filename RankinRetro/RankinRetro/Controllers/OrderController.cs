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

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }


        public IActionResult Details()
        { 
            var orders = _orderRepository.GetUserOrdersAsync().Result;
            List<OrderItem> ordersItems = new List<OrderItem>();
            foreach (var order in orders)
            {
                var orderItemTask = _orderRepository.GetOrderItemsAsync(order.OrderId).Result;
                foreach(var item in orderItemTask)
                {
                    ordersItems.Add(item);
                }
                
            }
            
            OrderDisplayViewModel orderVM = new OrderDisplayViewModel()
            {
                Orders = orders,
                OrderItems = ordersItems
            };

            return View(orderVM);
            
        }

        [HttpPost]
        public IActionResult RemoveOrder(Guid orderId) 
        {
            if (orderId != null)
            {
                _orderRepository.RemoveOrder(orderId);

            }
            else
            {
                throw new Exception("Order doesn't exist");
            }

            return RedirectToAction("Details");
        
        }


        [HttpPost]
        public IActionResult RemoveItem(Guid orderId, int productId)
        {
            if (orderId != null)
            {
                if (productId != 0)
                {
                    _orderRepository.RemoveItem(orderId, productId);
                }
                else
                {
                    throw new Exception($"Unable to remove item {productId}");
                }
            }
            else
            {
                throw new Exception("Order doesn't exist");
            }

            return RedirectToAction("Details");

        }
    }
}

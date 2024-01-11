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
            //Retrieve customer ID
            var customerId = _orderRepository.GetUserID();

            //Retrieve all the user's orders
            var orders = _orderRepository.GetUserOrdersAsync().Result;

            //Make a list to be populated with the items in each order
            List<OrderItem> ordersItems = new List<OrderItem>();

            //Retrieve the shipping address of each order
            var orderAddresses = _orderRepository.GetOrderAddressAsync(customerId).Result;

            //Retrieve the billing address of each order
            var billingAddresses = _orderRepository.GetBillingAddressAsync(customerId).Result;

            //For each to iterate over the orders and add the items in each order to a list
            foreach (var order in orders)
            {
                var orderItemTask = _orderRepository.GetOrderItemsAsync(order.OrderId).Result;
                foreach(var item in orderItemTask)
                {
                    //Add each item to a list
                    ordersItems.Add(item);
                }
            }
            
            OrderDisplayViewModel orderVM = new OrderDisplayViewModel()
            {
                Orders = orders,
                OrderItems = ordersItems,
                Address = orderAddresses,
                BillingAddress = billingAddresses
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

using Microsoft.AspNetCore.Mvc;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddItem(int productId, int qty = 1)
        {
            
            var cartCount = await _cartRepository.AddItem(productId, qty);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cartCount = await _cartRepository.RemoveItem(productId);
            return RedirectToAction("Details");
        }
        public async Task<IActionResult> AddOneItem(int productId)
        {
            var cartCount = await _cartRepository.AddOneItem(productId);
            return RedirectToAction("Details");
        }


        public async Task<IActionResult> Details()
        {
            var cart = await _cartRepository.GetUserCart();
            if (cart != null)
            {
                ViewBag.TotalPrice = cart.Details.Sum(x => x.Price * x.Quantity);
            }
            else
            {
                cart = new ShoppingCart
                {
                    CartItemId = 0,
                    CustomerId = null,
                    ShoppingCartId = 0,
                    Details = new List<ShoppingCartDetail>()

                };
            }
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepository.GetCartItemCount();
            return Ok(cartItem);
        }


    }
}

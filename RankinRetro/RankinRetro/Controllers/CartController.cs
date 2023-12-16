using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;

        public CartController(ICartRepository cartRepository, SignInManager<Customer> signInManager, UserManager<Customer> userManager)
        {
            _cartRepository = cartRepository;
            _signInManager = signInManager;
            _userManager = userManager;
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

        [HttpPost]
        public decimal DiscountAmount(string discountCode)
        {
            var discount = _cartRepository.GetDiscountAmount(discountCode);
            return discount;

        }


        public async Task<IActionResult> Checkout(string discountCode)
        {
           
            bool isActive = false;
            var cart = await _cartRepository.GetUserCart();


            if (cart != null)
            {
                ViewBag.TotalPrice = cart.Details.Sum(x => x.Price * x.Quantity);
                if (discountCode != null)
                {
                    var discountAmount = _cartRepository.GetDiscountAmount(discountCode);
                    ViewBag.TotalPriceDiscounted = Math.Round(cart.Details.Sum(x => x.Price * x.Quantity) * discountAmount, 2);
                    ViewBag.AmountDiscounted = ViewBag.TotalPrice - ViewBag.TotalPriceDiscounted;
                    ViewBag.DiscountCode = discountCode;
                    
                }
            }
               
                Customer signedInCustomer = await _signInManager.UserManager.GetUserAsync(User);
                if (User.Identity.IsAuthenticated)
                {
                    var userID = _signInManager.UserManager.GetUserId(User);
                    Customer customer = await _userManager.FindByIdAsync(userID);
                var cartVM = new CartDisplayViewModel
                    {
                        Email = signedInCustomer.Email,
                        Id = signedInCustomer.Id,
                        FirstName = customer.FirstName,
                        Surname = customer.Surname,
                        Title = customer.Title,
                        AddressFirstline = customer.AddressFirstline,
                        AddressSecondline = customer.AddressSecondline,
                        CityTown = customer.CityTown,
                        AddressPostcode = customer.AddressPostcode,
                        CartItems = cart.Details,

                    };

                    return View(cartVM);
                
                }
                else { return RedirectToAction("Details"); }
         
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

        [HttpPost]
        public async Task<IActionResult> CheckoutForm(decimal discountAmount)
        {
            bool checkedOut = await _cartRepository.Checkout(discountAmount);
            if (!checkedOut)
            {
                throw new Exception("Server did not compute request");
            }
            return RedirectToAction("Index", "Home");
        }


    }
}

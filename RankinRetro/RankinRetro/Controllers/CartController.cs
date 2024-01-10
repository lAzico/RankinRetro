using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Migrations;
using RankinRetro.Models;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class CartController : Controller
    {
        //Injections for user access and cart managing
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
            //Adding 1 item to the cart, the quantity parameter is used for future modification of the feature
            var cartCount = await _cartRepository.AddItem(productId, qty);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            //Removes 1 item from the cart depending on the product selected
            var cartCount = await _cartRepository.RemoveItem(productId);
            return RedirectToAction("Details");
        }


        public async Task<IActionResult> AddOneItem(int productId)
        {
            //Adds 1 item to the cart depeding on the product selecteds
            var cartCount = await _cartRepository.AddOneItem(productId);
            return RedirectToAction("Details");
        }



        //Application of discount
        [HttpPost]
        public decimal DiscountAmount(string discountCode)
        {
            //Using the discount code, the discount amount is returned in decimal format
            var discount = _cartRepository.GetDiscountAmount(discountCode);
            return discount;
        }



        //Checkout page with a parameter for the discount code, which applies a discount to the items in the cart
        public async Task<IActionResult> Checkout(string discountCode)
        {
            //Cart is retrived from the cart repository
            var cart = await _cartRepository.GetUserCart();

            if (cart != null)
            {

                //Using the ViewBag a new variable is bade for the total price of the cart, multiplying the price of the items by the quantity
                ViewBag.TotalPrice = cart.Details.Sum(x => x.Price * x.Quantity);

                //If a discount code was passed into the view then this statement will execute
                if (discountCode != null)
                {
                   //Using the discount code supplied from the view's discount parameter
                    var discountAmount = _cartRepository.GetDiscountAmount(discountCode);
                    
                    //ViewBag variables
                    ViewBag.DiscountAmount = discountAmount;
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
                var CartVM = new CartDisplayViewModel
                {
                    Email = signedInCustomer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname,
                    Title = customer.Title,
                    AddressFirstline = customer.AddressFirstline,
                    AddressSecondline = customer.AddressSecondline,
                    CityTown = customer.CityTown,
                    AddressPostcode = customer.AddressPostcode,
                    CartItems = cart.Details,
                };

                return View(CartVM);

            }
            else { return RedirectToAction("Details"); }

        }

        //Form for checking out cart details, billing details and shipping details
        [HttpPost]
        public async Task<IActionResult> CheckoutCart(decimal ddiscountAmount, CartDisplayViewModel CartVM)
        {
            var CustomerId = _userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Checkout", "Cart");
            }
            else
            {
                //Check out the cart with the 
                var checkedOut = await _cartRepository.Checkout(ddiscountAmount);

                var BillingAddress = new OrderBillingAddress
                {
                    FirstName = CartVM.FirstName,
                    Surname = CartVM.Surname,
                    AddressFirstline = CartVM.AddressFirstline,
                    AddressSecondline = CartVM.AddressSecondline,
                    AddressPostcode = CartVM.AddressPostcode,
                    CityTown = CartVM.CityTown,
                    Title = CartVM.Title,
                    CustomerId = CustomerId,
                    OrderId = checkedOut.OrderId,
                    Email = CartVM.Email,
                };

                var ShippingAddress = new OrderAddress
                {
                    ShippingFirstName = CartVM.ShippingFirstName,
                    ShippingSurname = CartVM.ShippingSurname,
                    ShippingAddressFirstline = CartVM.ShippingAddressFirstline,
                    ShippingAddressSecondline = CartVM.ShippingAddressSecondline,
                    ShippingAddressPostcode = CartVM.ShippingAddressPostcode,
                    ShippingCityTown = CartVM.ShippingCityTown,
                    ShippingTitle = CartVM.ShippingTitle,
                    CustomerId = CustomerId,
                    OrderId = checkedOut.OrderId,
                    Email = CartVM.ShippingEmail
                };

                _cartRepository.AddBillingAddress(BillingAddress);
                _cartRepository.AddOrderAddress(ShippingAddress);


                return RedirectToAction("Details", "Order");
            }


        }

        //View for details
        public async Task<IActionResult> Details()
        {
            var cart = await _cartRepository.GetUserCart();
            if (cart != null)
            {
                ViewBag.TotalPrice = cart.Details.Sum(x => x.Price * x.Quantity);
            }
            else
            {
                //If the cart doesn't exist then make a new empty one
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

    
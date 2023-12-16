using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Customer> _userManager;

        public CartRepository(ApplicationDbContext context, HttpContextAccessor httpContextAccessor, UserManager<Customer> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private string GetUserID()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);
            return userId;
        }

        public  decimal GetDiscountAmount(string Discount)
        {
            var discounts =  _context.Discounts.ToListAsync();
            var discountAmount = discounts.Result.FirstOrDefault(x => x.DiscountName == Discount).DiscountAmount;
            return discountAmount;
        }


        public async Task<int> AddItem(int productID, int qty)
        {
            var userId = GetUserID();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var cart = await GetCart(userId);
                    if (cart == null)
                    {
                        cart = new ShoppingCart
                        {
                            CustomerId = userId,
                        };
                        _context.ShoppingCarts.Add(cart);
                    }

                    _context.SaveChanges();

                    var cartItem = _context.ShoppingCartDetail.FirstOrDefault(a => a.ShoppingCartId == cart.ShoppingCartId && a.ProductId == productID);
                    if (cartItem != null)
                    {
                        cartItem.Quantity += qty;
                    }
                    else
                    {
                        var product = _context.Products.Find(productID);
                        cartItem = new ShoppingCartDetail
                        {
                            ProductId = productID,
                            ShoppingCartId = cart.ShoppingCartId,
                            Quantity = qty,
                            Price = (product.Price)
                        };
                        _context.ShoppingCartDetail.Add(cartItem);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    throw new Exception("Invalid user session");
                }
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public Task<bool> Checkout()
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.CustomerId == userId);
            return cart;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserID();
            }
            var data = await (from cart in _context.ShoppingCarts
                              join ShoppingCartDetail in _context.ShoppingCartDetail
                              on cart.ShoppingCartId equals ShoppingCartDetail.Id
                              select new { ShoppingCartDetail.Id }).ToListAsync();
            return data.Count;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserID();
            if(userId != null)
            {
                var shoppingCart = await _context.ShoppingCarts.Include(a => a.Details)
                                                               .ThenInclude(a => a.Product)
                                                               .Where(a => a.CustomerId == userId).FirstOrDefaultAsync();
                
                return shoppingCart;

            }
            else
            {
                throw new Exception("Invalid user");
            }

        }

        public async Task<int> RemoveItem(int productID)
        {
            string userId = GetUserID();
            if (!string.IsNullOrEmpty(userId))
            {
                var cart = await GetCart(userId);
                if (cart != null)
                {
                    var cartItem = _context.ShoppingCartDetail.FirstOrDefault(a => a.ShoppingCartId == cart.ShoppingCartId && a.ProductId == productID);
                    if (cartItem == null)
                    {
                        throw new Exception("No items in cart");
                    }
                    else if (cartItem.Quantity == 1)
                    {
                        _context.ShoppingCartDetail.Remove(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity = cartItem.Quantity - 1;
                        _context.SaveChanges();
                    }
                    _context.SaveChanges();
                }
                var cartItemCount = await GetCartItemCount(userId);
                return cartItemCount;
            }
            else { throw new Exception("Invalid user "); }
        }

        public async Task<int> AddOneItem(int productID)
        {
            string userId = GetUserID();
            if (!string.IsNullOrEmpty(userId))
            {
                var cart = await GetCart(userId);
                if (cart != null)
                {
                    var cartItem = _context.ShoppingCartDetail.FirstOrDefault(a => a.ShoppingCartId == cart.ShoppingCartId && a.ProductId == productID);
                    if (cartItem == null)
                    {
                        throw new Exception("No items in cart");
                    }
                    else
                    {
                        cartItem.Quantity = cartItem.Quantity + 1;
                        _context.SaveChanges();
                    }
                    _context.SaveChanges();
                }
                var cartItemCount = await GetCartItemCount(userId);
                return cartItemCount;
            }
            else { throw new Exception("Invalid user "); }
        }
    }
}

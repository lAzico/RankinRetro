﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Migrations;
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

        public async Task<Order> Checkout(decimal discountAmount)
        {
            using var transaction = _context.Database.BeginTransaction();

            var userID = GetUserID();

            if (userID == null)
            {
                throw new Exception("No user logged in");

            }

            var cart = await GetCart(userID);
            if (cart == null)
            {
                throw new Exception("Cart is invalid");
            }

            var cartDetails = _context.ShoppingCartDetail.Where(a => a.ShoppingCartId == cart.ShoppingCartId).ToList();
            if (cartDetails.Count == 0)
            {
                throw new Exception("No items in cart");
            }
            if (discountAmount > 0)
            {
                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerId = userID,
                    OrderDate = DateTime.UtcNow,
                    Status = Data.Enum.Status.Pending,
                    Total = Math.Round(cartDetails.Sum(x => x.Price * x.Quantity) * discountAmount, 2),
                    DiscountAmount = discountAmount,
                };

                var Address = new OrderAddress
                {
                    OrderId = order.OrderId,
                    CustomerId = userID,
                    

                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                foreach (var item in cartDetails)
                {
                    var productName = _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId).Result.Name;
                    var productImageURL = _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId).Result.ImageURL;
                    var orderDetail = new OrderItem
                    {
                        URL = productImageURL,
                        Name = productName,
                        ProductId = item.ProductId,
                        OrderId = order.OrderId,
                        Quantity = item.Quantity,
                        Price = item.Price * discountAmount,
                        DiscountAmount = discountAmount
                    };
                    _context.OrderItems.Add(orderDetail);
                }
                _context.RemoveRange(cartDetails);
                _context.SaveChanges();

                transaction.Commit();
                return order;
            }

            else
            {
                var order = new Order
                {
                    CustomerId = userID,
                    OrderDate = DateTime.UtcNow,
                    Status = Data.Enum.Status.Pending,
                    Total = Math.Round(cartDetails.Sum(x => x.Price * x.Quantity), 2)
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                foreach (var item in cartDetails)
                {
                    var productName = _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId).Result.Name;
                    var productImageURL = _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId).Result.ImageURL;
                    var orderDetail = new OrderItem
                    {
                        URL = productImageURL,
                        Name = productName,
                        ProductId = item.ProductId,
                        OrderId = order.OrderId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    _context.OrderItems.Add(orderDetail);
                }
                _context.RemoveRange(cartDetails);
                _context.SaveChanges();

                transaction.Commit();
                return order;
            }

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

        public bool AddBillingAddress(OrderBillingAddress billingAddress)
        {
            _context.OrderBillingAddresses.Add(billingAddress);
            return Save();
        }

        public bool AddOrderAddress(OrderAddress orderAddress)
        {
            _context.OrderAddresses.Add(orderAddress);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

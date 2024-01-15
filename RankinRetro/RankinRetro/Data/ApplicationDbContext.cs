using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RankinRetro.Models;

namespace RankinRetro.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ReviewAndRating> ReviewsAndRatings { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<AzureStorageConfig> Configs { get; set; }
        public DbSet<ShoppingCartDetail> ShoppingCartDetail { get; set;}
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<OrderBillingAddress> OrderBillingAddresses { get; set; }
        public DbSet<Types> Types { get; set; }

    }
}

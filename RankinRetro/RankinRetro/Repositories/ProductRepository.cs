using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextAccessor _httpContentAccessor;
        private readonly UserManager<Customer> _userManager;

        public ProductRepository(ApplicationDbContext context, HttpContextAccessor httpContentAccessor, UserManager<Customer> userManager)
        {
            _context = context;
            _httpContentAccessor = httpContentAccessor;
            _userManager = userManager;
        }
        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
           return await _context.Products.ToListAsync();
           
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int id)
        {
            return await _context.Products.Where(c => c.CategoryId.Equals(id)).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> GetByIdNoTrackingAsync(int id)
        {
            //AsNoTracking tells the database not to track any changes made, making it a read only query
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }



        public async Task<bool> Update(Product product)
        {

            var user = _httpContentAccessor.HttpContext.User;
            var userID = _userManager.GetUserId(user);

            List<OrderItem> orderItemProducts = _context.OrderItems.Where(x => x.ProductId == product.ProductId).ToList();

            if (orderItemProducts.Any())
            {
                foreach (var orderProduct in orderItemProducts)
                {
                    orderProduct.Price = product.Price;
                    orderProduct.Name = product.Name;
                    orderProduct.URL = product.ImageURL;
                    _context.Update(orderProduct);
                }
            }
            _context.Update(product);
            return Save();
        }

        public async Task<AzureStorageConfig> GetAzureStorageConfigAsync(string id)
        {
            return await _context.Configs.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

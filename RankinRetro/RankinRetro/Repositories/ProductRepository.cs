using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
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
            return await _context.Products
        .Where(c => c.CategoryId.Equals(id)).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}

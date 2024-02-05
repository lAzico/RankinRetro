using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context) 
        {
            _context = context;

        }

        //Async task to return a list of products
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        //Async task to return a list of categories
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        //Async task to return a list of brands
        public async Task<List<Brand>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<List<Category>> GetByCategory(int categoryId)
        {
            return await _context.Categories.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Brand>> GetByBrand(int brandId)
        {
            return await _context.Brands.Where(x => x.BrandId == brandId).ToListAsync();
        }

    }
}

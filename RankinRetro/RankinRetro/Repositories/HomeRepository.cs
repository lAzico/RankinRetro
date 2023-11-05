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
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}

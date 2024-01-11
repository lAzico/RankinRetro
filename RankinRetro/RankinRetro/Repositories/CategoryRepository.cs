using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{

    //WIP - Will be added when adding search features
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Category>> GetAllCategories()
        {
            return _context.Categories.ToListAsync();
        }

        public Task<List<Product>> GetAllProducts()
        {
            return _context.Products.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }
    

        public Task<Category> GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

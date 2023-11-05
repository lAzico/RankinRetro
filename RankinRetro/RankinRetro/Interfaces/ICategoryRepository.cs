using Microsoft.EntityFrameworkCore;
using RankinRetro.Data;
using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Product>> GetAllProducts();
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<Product> GetProductById(int id);


    }
}

using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public interface IHomeRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Category>> GetCategories();
        Task<List<Brand>> GetBrands();
    }
}

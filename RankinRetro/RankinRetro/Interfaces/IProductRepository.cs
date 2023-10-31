using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByIdAsync(int id);

        Task<Product> GetByIdNoTrackingAsync(int id);
        Task<IEnumerable<Product>> GetByCategory(int id);

        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();
    }
}

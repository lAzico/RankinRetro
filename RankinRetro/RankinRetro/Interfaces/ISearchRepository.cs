using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface ISearchRepository
    {
        List<Product> SearchProduct(List<int> types, List<int> brands, List<int> categories); 

    }
}

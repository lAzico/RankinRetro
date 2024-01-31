using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface ISearchRepository
    {
        List<Product> SearchProduct(List<string> types, List<string> brands, List<string> categories); 

    }
}

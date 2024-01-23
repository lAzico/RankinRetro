using RankinRetro.Models;

namespace RankinRetro.ViewModels;
public class SearchViewModel
{
    public List<Product> searchProducts { get; set; }
    public List<Product> Products { get; set; }
    public List<Types> types {  get; set; }
    public List<Brand> brands { get; set; }
    public List<Category> categories { get; set; }

    public List<int>? typesSearch { get; set; }
    public List<int>? brandsSearch { get; set; }
    public List<int>? categoriesSearch { get; set; }
    public List<Product> productsSearched { get; set; }
}
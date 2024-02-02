using RankinRetro.Models;

namespace RankinRetro.ViewModels;
public class SearchViewModel
{
    public List<Product> Products { get; set; }
    public List<Types> types {  get; set; }
    public List<Brand> brands { get; set; }
    public List<Category> categories { get; set; }

    public List<string>? typesSearch { get; set; }
    public List<string>? brandsSearch { get; set; }
    public List<string>? categoriesSearch { get; set; }
}
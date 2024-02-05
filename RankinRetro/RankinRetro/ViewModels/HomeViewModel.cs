using RankinRetro.Models;

namespace RankinRetro.ViewModels
{
    public class HomeViewModel
    {

        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Brand>? Brands { get; set; }

        //Search features
        public string brand {  get; set; }
        public string category { get; set; }
        public string type { get; set; }

    }

}


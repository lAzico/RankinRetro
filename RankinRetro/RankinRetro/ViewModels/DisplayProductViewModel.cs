using RankinRetro.Data.Enum;
using RankinRetro.Models;

namespace RankinRetro.ViewModels
{
    public class DisplayProductViewModel
    {

            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

            
            public int BrandId { get; set; }
            public List<Brand> brands { get; set; }
            public int CategoryId { get; set; }
            public List<Category> categories { get; set; }
            public int TypeId { get; set; }
            public List<Types> Types { get; set; }
            public Size Size { get; set; }
            public Colour Colour { get; set; }
            public Material Material { get; set; }
            public string ImageURL { get; set; }

        }
    }


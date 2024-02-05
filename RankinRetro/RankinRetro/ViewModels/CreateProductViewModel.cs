using RankinRetro.Data.Enum;
using RankinRetro.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.ViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public int BrandId { get; set; }

        //This list is so the brand ID can be compared against the brands for frontend display
        public List<Brand> Brands { get; set; }

        public int CategoryId { get; set; }
        //This list is so the category ID can be compared against the categories for frontend display

        public List<Category> Categories { get; set; }
        public int TypeId { get; set; }
        //This list is so the type ID can be compared against the types for frontend display
        public List<Types> Types { get; set; }
        public Size Size { get; set; }
        public Colour Colour { get; set; }
        public Material Material { get; set; }
        //IFormFile to allow files to be uploaded, this will be converted to a string URL with the image service
        public IFormFile ImageURL { get; set; }


    }
}


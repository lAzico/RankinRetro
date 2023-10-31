using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.ViewModels
{
    public class EditProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public int CategoryId { get; set; }
        public Size Size { get; set; }
        public Colour Colour { get; set; }
        public Material Material { get; set; }
        public string ImageURL { get; set; }
    }
}

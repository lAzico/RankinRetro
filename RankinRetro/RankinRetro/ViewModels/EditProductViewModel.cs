﻿using RankinRetro.Data.Enum;
using RankinRetro.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int TypeId { get; set; }
        public List<Types> Types { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public Size Size { get; set; }
        public Colour Colour { get; set; }
        public Material Material { get; set; }

        public IFormFile? Image { get; set; }
    }
}

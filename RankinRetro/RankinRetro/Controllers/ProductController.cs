using Microsoft.AspNetCore.Mvc;
using RankinRetro.Data.Enum;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public IActionResult Details(int id)
        {
            var results = _productRepository.GetByIdAsync(id);
            return View(results);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(product);
            return View(product);
        }
        public IActionResult Create() 
        { 

            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var categories = await _productRepository.GetAllCategories();
            var brands = await _productRepository.GetAllBrands();
            if (product == null) return RedirectToAction("Home");
            var productVM = new EditProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Brands = brands.ToList(),
                Categories = categories.ToList(),
                Size = product.Size,
                Colour = product.Colour,
                Material = product.Material,
                ImageURL = product.ImageURL
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Product");
                return View("Edit", productVM);
            }

            var userProduct = await _productRepository.GetByIdNoTrackingAsync(id);
            

            if (userProduct != null)
            {
                var product = new Product
                {
                    Name = userProduct.Name,
                    Description = userProduct.Description,
                    Price = userProduct.Price,
                    BrandId = userProduct.BrandId,
                    CategoryId = userProduct.CategoryId,
                    Size = userProduct.Size,
                    Colour = userProduct.Colour,
                    Material = userProduct.Material,
                    ImageURL = userProduct.ImageURL
                };
                _productRepository.Update(product);
                return RedirectToAction("Home");
            }
            else
            {
                return View(productVM);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Home");
            }

            var product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                CategoryId = productVM.CategoryId,
                Size = productVM.Size,
                Colour = productVM.Colour,
                Material = productVM.Material,
                ImageURL = productVM.ImageURL
            };

            _productRepository.Add(product);
            return RedirectToAction("Home");
        }


    }
}

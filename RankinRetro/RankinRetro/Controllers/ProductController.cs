using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RankinRetro.Data.Enum;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using RankinRetro.Services;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly AzureStorageConfig _config;
        private readonly ImageService _imageService;


        public ProductController(IProductRepository productRepository, IOptions<AzureStorageConfig> config)
        {
            _productRepository = productRepository;
            _config = config.Value;

        }



        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var brands = await _productRepository.GetAllBrands();
            var categories = await _productRepository.GetAllCategories();
            var results = new DisplayProductViewModel

            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                brands = brands.ToList(),
                categories = categories.ToList(),
                Size = product.Size,
                Colour = product.Colour,
                Material = product.Material,
                ImageURL = product.ImageURL
            };
            return View(results);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(product);
            return View(product);
        }

        public async Task<IActionResult> DeleteProductHome(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(product);
            return RedirectToAction("Index", "Home");
        }





        public async Task<IActionResult> Create()
        {
            var productVM = new CreateProductViewModel();
            var categories = await _productRepository.GetAllCategories();
            var brands = await _productRepository.GetAllBrands();


            productVM.Brands = brands.ToList();
            productVM.Categories = categories.ToList();
            return View(productVM);
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
            return RedirectToAction("Index", "Home");
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
                    ProductId = id,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    BrandId = productVM.BrandId,
                    CategoryId = productVM.CategoryId,
                    Size = productVM.Size,
                    Colour = productVM.Colour,
                    Material = productVM.Material,
                    ImageURL = productVM.ImageURL
                };
                _productRepository.Update(product);
                return RedirectToPage("");
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

            var categories = await _productRepository.GetAllCategories();
            var brands = await _productRepository.GetAllBrands();
            bool isUploaded = false;
            var configTask = _productRepository.GetAzureStorageConfigAsync("1");
            var config = await configTask;

            try
            {

                if (_config.AccountKey == string.Empty || _config.AccountName == string.Empty)
                {
                    return BadRequest("Sorry, can't retrieve your storage details from appsettings.js");
                }

                if (_config.ImageContainer == string.Empty)
                {
                    return BadRequest("Image container is not configured");
                }

                if (ImageService.isImage(productVM.ImageURL))
                {
                    if (productVM.ImageURL.Length > 0)
                    {
                        using (Stream stream = productVM.ImageURL.OpenReadStream())
                        {
                            isUploaded = await ImageService.UploadFileToStorage(stream, productVM.ImageURL.FileName, productVM.Name, config);
                            productVM.Brands = brands.ToList();
                            productVM.Categories = categories.ToList();

                            if (isUploaded)
                            {
                                var URLList = await ImageService.GetThumbNailUrls(config, productVM.Name);
                                foreach (var url in URLList)
                                {
                                    if (URLList.Contains(productVM.Name))
                                    {
                                        var URLProduct = url;

                                        var product = new Product
                                        {
                                            Name = productVM.Name,
                                            Description = productVM.Description,
                                            Price = productVM.Price,
                                            BrandId = productVM.BrandId,
                                            CategoryId = productVM.CategoryId,
                                            Size = productVM.Size,
                                            Colour = productVM.Colour,
                                            Material = productVM.Material,
                                            ImageURL = URLProduct
                                        };

                                        _productRepository.Add(product);
                                        return Redirect("Details/" + product.ProductId.ToString());
                                    }
                                    else
                                    {
                                        return BadRequest("The image couldn't be uploaded to storage");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return new UnsupportedMediaTypeResult();
                    }
                }
                

                if (isUploaded)
                {
                    if (_config.ThumbnailContainer != string.Empty)
                    {
                        return new AcceptedAtActionResult("GetThumbnails", "Images", null, null);
                    }
                    else
                    {
                        return new AcceptedResult();
                    }
                }
                else
                {
                    return BadRequest("The image couldn't be uploaded to storage");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Components.Web;
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
            //The config is the Azure storage account's access information held in the SQL server
            _config = config.Value;
        }


        //Depending on the product's ID viewed, the details are shown
        public async Task<IActionResult> Details(int id)
        {
            
            var product = await _productRepository.GetByIdAsync(id);
            var brands = await _productRepository.GetAllBrands();
            var categories = await _productRepository.GetAllCategories();
            var types = await _productRepository.GetAllTypes();
            var results = new DisplayProductViewModel

            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                TypeId = product.TypeId,

                /*The brands list is used to be able to compare the brand ID
                against the enum of brands so the user can see the name of the brands, not the ID*/
                brands = brands.ToList(),

                //The categories list is used in the same way as the brands list
                categories = categories.ToList(),

                Types = types.ToList(),
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
            var types = await _productRepository.GetAllTypes();


            productVM.Brands = brands.ToList();
            productVM.Categories = categories.ToList();
            productVM.Types = types.ToList();
            return View(productVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var categories = await _productRepository.GetAllCategories();
            var brands = await _productRepository.GetAllBrands();
            var types = await _productRepository.GetAllTypes();
            if (product == null)
            {
                return RedirectToAction("Home");
            }
            var productVM = new EditProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Brands = brands.ToList(),
                Categories = categories.ToList(),
                TypeId = product.TypeId,
                Types = types.ToList(),
                Size = product.Size,
                Colour = product.Colour,
                Material = product.Material,
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
            //Get product from the ID
            var userProduct = await _productRepository.GetByIdNoTrackingAsync(id);

            //Retrieve the Azure config
            var configTask = _productRepository.GetAzureStorageConfigAsync("1");
            var config = await configTask;
            
            bool isUploaded = false;

            if (productVM.Image != null)
            {

                try
                {
                    //If statements for invalid config information
                    if (config.AccountKey == string.Empty || config.AccountName == string.Empty)
                    {
                        return BadRequest("Sorry, can't retrieve your storage details from appsettings.js");
                    }
                    if (config.ImageContainer == string.Empty)
                    {
                        return BadRequest("Image container is not configured");
                    }

                    //Check if the file uploaded is an image
                    if (ImageService.isImage(productVM.Image))
                    {
                        if (productVM.Image.Length > 0)
                        {
                            //Read the file
                            using (Stream stream = productVM.Image.OpenReadStream())
                            {

                                //Upload the file to the Azure container
                                isUploaded = await ImageService.UploadFileToStorage(stream, productVM.Image.FileName, productVM.Name, config);

                                if (isUploaded)
                                {
                                    //Retrieve the URLs from the folder of the product (this can be multiple images from previously uploaded versions)
                                    var thumbnailUrls = await ImageService.GetThumbNailUrls(config, productVM.Name);
                                    foreach (var url in thumbnailUrls)
                                    {
                                        //Check if there is a URL: which contains the new file name which was uploaded
                                        if (url.Contains("https://" + config.AccountName + ".blob.core.windows.net/" + config.ImageContainer + "/" + productVM.Name + "/" + productVM.Image.FileName))


                                        {
                                            var URLProduct = url;


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
                                                    TypeId = productVM.TypeId,
                                                    Size = productVM.Size,
                                                    Colour = productVM.Colour,
                                                    Material = productVM.Material,


                                                    //Set the new URL to the updated image URL
                                                    ImageURL = URLProduct
                                                };

                                                await _productRepository.Update(product);
                                                return RedirectToAction("Index", "Home");
                                            }
                                        }
                                    }
                                }



                            }

                        }

                    }
                    return BadRequest("Invalid input or image format");
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

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
                    TypeId= productVM.TypeId,
                    Size = productVM.Size,
                    Colour = productVM.Colour,
                    Material = productVM.Material,
                    ImageURL = userProduct.ImageURL
                };
                await _productRepository.Update(product);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Could not update product");
                return RedirectToAction("Index", "Home");
            }
        }
        


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Home");
            }


            bool isUploaded = false;

            var configTask = _productRepository.GetAzureStorageConfigAsync("1");
            var config = await configTask;

            try
            {

                if (config.AccountKey == string.Empty || config.AccountName == string.Empty)
                {
                    return BadRequest("Sorry, can't retrieve your storage details from appsettings.js");
                }

                if (config.ImageContainer == string.Empty)
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


                            if (isUploaded)
                            {
                                var URLList = await ImageService.GetThumbNailUrls(config, productVM.Name);
                                foreach (var url in URLList)
                                {
                                    Console.Write(url);
                                    var consoleURL = "https://" + config.AccountName + ".blob.core.windows.net/" + config.ImageContainer + "/" + productVM.Name + "/" + productVM.ImageURL.FileName;
                                    if (URLList.Contains("https://" + config.AccountName + ".blob.core.windows.net/" + config.ImageContainer + "/" + productVM.Name + "/" + productVM.ImageURL.FileName))


                                    {
                                        var URLProduct = url;

                                        var product = new Product
                                        {
                                            Name = productVM.Name,
                                            Description = productVM.Description,
                                            Price = productVM.Price,
                                            BrandId = productVM.BrandId,
                                            CategoryId = productVM.CategoryId,
                                            TypeId = productVM.TypeId,
                                            Size = productVM.Size,
                                            Colour = productVM.Colour,
                                            Material = productVM.Material,
                                            ImageURL = URLProduct
                                        };

                                        _productRepository.Add(product);
                                        //Redirect the user to the new product's details page
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

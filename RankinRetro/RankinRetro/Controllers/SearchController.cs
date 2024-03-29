﻿using Microsoft.AspNetCore.Mvc;
using RankinRetro.Interfaces;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IProductRepository _productRepository;

        public SearchController(ISearchRepository searchRepository, IProductRepository productRepository)
        {
            _searchRepository = searchRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index(SearchViewModel newSearchVM) 
        {
            var products = _productRepository.GetAllProducts().Result.ToList();
            var types = _productRepository.GetAllTypes().Result.ToList();
            var brands = _productRepository.GetAllBrands().Result.ToList();
            var categories = _productRepository.GetAllCategories().Result.ToList();


            if (newSearchVM.brands != null)
            {
                return View(newSearchVM);
            }
            else
            {
                SearchViewModel searchVM = new SearchViewModel()
                {
                    Products = products,
                    types = types,
                    brands = brands,
                    categories = categories
                };
                return View(searchVM);
            }
        }

        [HttpPost]
        public IActionResult Results(string[] typesSearch, string[] brandsSearch, string[] categoriesSearch)
        {
            //Filtered products
            var productsList = _searchRepository.SearchProduct(typesSearch.ToList(), brandsSearch.ToList(), categoriesSearch.ToList());

            var products = _productRepository.GetAllProducts().Result.ToList();
            var types = _productRepository.GetAllTypes().Result.ToList();
            var brands = _productRepository.GetAllBrands().Result.ToList();
            var categories = _productRepository.GetAllCategories().Result.ToList();

            //Return the ViewModel with products if there has been no filter applied
            if (productsList.Count == 0)
            {
                SearchViewModel searchVM = new SearchViewModel()
                {
                    Products = products,
                    types = types,
                    brands = brands,
                    categories = categories,
                    categoriesSearch = categoriesSearch.ToList(),
                    typesSearch = typesSearch.ToList(),
                    brandsSearch = brandsSearch.ToList()

                };
                return PartialView("_FilteredProductsPartial", searchVM);
            }

            else
            {
                SearchViewModel searchVM = new SearchViewModel()
                {
                    Products = productsList,
                    types = types,
                    brands = brands,
                    categories = categories,
                    categoriesSearch = categoriesSearch.ToList(),
                    typesSearch = typesSearch.ToList(),
                    brandsSearch = brandsSearch.ToList()

                };
                return PartialView("_FilteredProductsPartial", searchVM);

            }
           

        }
    }
}

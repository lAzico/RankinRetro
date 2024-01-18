using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index() 
        {
            var products = _productRepository.GetAllProducts().Result.ToList();
            var types = _productRepository.GetAllTypes().Result.ToList();
            var brands = _productRepository.GetAllBrands().Result.ToList();
            var categories = _productRepository.GetAllCategories().Result.ToList();

            SearchViewModel searchVM = new SearchViewModel()
            {
                Products = products,
                types = types,
                brands = brands,
                categories = categories
            };
            return View(searchVM);
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel searchVM)
        {
            var productsList = _searchRepository.SearchProduct(searchVM.typesSearch, searchVM.brandsSearch, searchVM.categoriesSearch);
            searchVM.productsSearched = productsList;
            
            return RedirectToAction("Index", searchVM);

        }
    }
}

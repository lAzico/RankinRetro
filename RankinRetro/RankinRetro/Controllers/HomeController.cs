using Microsoft.AspNetCore.Mvc;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using RankinRetro.Repositories;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Products = await _homeRepository.GetAllProducts(),
                Categories = await _homeRepository.GetCategories()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankinRetro.Data;
using RankinRetro.Models;
using RankinRetro.ViewModels;

namespace RankinRetro.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            
            if (!ModelState.IsValid)
            return View(loginViewModel);
            

            var user = await _userManager.FindByNameAsync(loginViewModel.EmailAddress);
            if (user != null) 
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
                return View(loginViewModel);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
            return View(loginViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = await _userManager.FindByNameAsync(registerViewModel.EmailAddress);
            if (user != null) 
            {
                ModelState.AddModelError(string.Empty, "Email address is already in use");
                return View(registerViewModel);
            }

            var newUser = new Customer { Email = registerViewModel.EmailAddress, UserName = registerViewModel.EmailAddress, FirstName = registerViewModel.FirstName,
                Surname = registerViewModel.Surname, Title = registerViewModel.Title, AddressFirstline = registerViewModel.AddressFirstline,
                AddressSecondline = registerViewModel.AddressSecondline, CityTown = registerViewModel.CityTown, AddressPostcode = registerViewModel.AddressPostcode,
                Gender = registerViewModel.Gender, Phone = registerViewModel.Phone};

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!newUserResponse.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Could not create new User.");
                return View(registerViewModel);
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

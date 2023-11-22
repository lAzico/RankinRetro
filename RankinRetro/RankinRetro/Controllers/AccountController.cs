using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        

        public async Task<IActionResult> Profile()
        {

            if (User.Identity.IsAuthenticated)
            {
                var userID = _signInManager.UserManager.GetUserId(User);
                Customer customer = await _userManager.FindByIdAsync(userID);
                var profileVM = new ProfileViewModel
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname,
                    Title = customer.Title,
                    AddressFirstline = customer.AddressFirstline,
                    AddressSecondline = customer.AddressSecondline,
                    CityTown = customer.CityTown,
                    AddressPostcode = customer.AddressPostcode,
                    Gender = customer.Gender,
                    Phone = customer.Phone
                };

                return View(profileVM);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Edit()
        {
            Customer signedInCustomer = await _signInManager.UserManager.GetUserAsync(User);
            if (signedInCustomer == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (User.Identity.IsAuthenticated)
            {
                var userID = _signInManager.UserManager.GetUserId(User);
                Customer customer = await _userManager.FindByIdAsync(userID);
                var profileVM = new ProfileViewModel
                {

                    Id = signedInCustomer.Id,
                    Email = signedInCustomer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname,
                    Title = customer.Title,
                    AddressFirstline = customer.AddressFirstline,
                    AddressSecondline = customer.AddressSecondline,
                    CityTown = customer.CityTown,
                    AddressPostcode = customer.AddressPostcode,
                    Gender = customer.Gender,
                    Phone = customer.Phone
                };

                return View(profileVM);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
public async Task<IActionResult> Edit(ProfileViewModel profileVM)
{
    if (User.Identity.IsAuthenticated)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit customer details");
            return View("Edit", profileVM);
        }

        Customer signedInCustomer = await _signInManager.UserManager.GetUserAsync(User);
        if (signedInCustomer != null)
        {
           
            signedInCustomer.FirstName = profileVM.FirstName;
            signedInCustomer.Surname = profileVM.Surname;
            signedInCustomer.Title = profileVM.Title;
            signedInCustomer.AddressFirstline = profileVM.AddressFirstline;
            signedInCustomer.AddressSecondline = profileVM.AddressSecondline;
            signedInCustomer.CityTown = profileVM.CityTown;
            signedInCustomer.AddressPostcode = profileVM.AddressPostcode;
            signedInCustomer.Gender = profileVM.Gender;
            signedInCustomer.Phone = profileVM.Phone;

            var result = await _userManager.UpdateAsync(signedInCustomer);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update customer details");
                return View(profileVM);
            }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    ModelState.AddModelError(string.Empty, "Invalid user");
    return View(profileVM);
}
        public IActionResult Error()
        {
            return View();
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

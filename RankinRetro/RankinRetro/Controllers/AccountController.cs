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
        //Injections for handling user access
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        //Profile page
        public async Task<IActionResult> Profile()
        {
            //If the User who loads the profile is authenticated then the if statement will execute
            if (User.Identity.IsAuthenticated)
            {
                //User ID retrieved using the sign in manager
                var userID = _signInManager.UserManager.GetUserId(User);
                //A new customer model is made and the details retrieved using the User ID
                Customer customer = await _userManager.FindByIdAsync(userID);

                //Using the profile view model the fields are populated using the retrieved customer
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

        //Edit page
        public async Task<IActionResult> Edit()
        {
            //New customer model from the signed in user
            Customer signedInCustomer = await _signInManager.UserManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                //User ID retrieved from the sign in manager
                var userID = _signInManager.UserManager.GetUserId(User);
                //A new customer model is made and the details retrieved using the User ID
                Customer customer = await _userManager.FindByIdAsync(userID);
                //Using the profile view model the fields are populated using the retrieved customer model and signed in customer model
                var profileVM = new ProfileViewModel
                {
                    Email = signedInCustomer.Email,
                    Id = signedInCustomer.Id,
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


        //Post form for the Edit page
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel profileVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                //If the form model posted is invalid then the following if statement will be executed
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to edit customer details");
                    return View("Edit", profileVM);
                }


                /*Using profile view model passed through the parameter, the signed in user's details
                  are modified*/
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

                    //Using the modified user details, the user is updated
                    var result = await _userManager.UpdateAsync(signedInCustomer);

                    if (result.Succeeded)
                    {
                        //Redirect to the profile page once updated
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        //Returns an error message if the profile doesn't get updated
                        ModelState.AddModelError(string.Empty, "Failed to update customer details");
                        return View(profileVM);
                    }
                }
                else
                {
                    //If the user isn't authenticated then the home page is returned
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid user");
            return View(profileVM);
        }

        //Error page
        public IActionResult Error()
        {
            return View();
        }

        //Login page
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        //Register page
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        //Post form for the Login page
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            
            if (!ModelState.IsValid)
            return View(loginViewModel);
            
            //User is searched for using the data entered into the email address field
            var user = await _userManager.FindByNameAsync(loginViewModel.EmailAddress);
            if (user != null) 
            {
                //Using the user manager's password check method, the user's password is checked against the data entered into the password field
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Using the user manager the user is securely signed in and when succeeded a session token is made and redirected to the home page
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //If the user's password isn't correct then an error is returned
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
                return View(loginViewModel);
            }
            //If the user's email isn't correct then an error is returned
            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
            return View(loginViewModel);
        }

        //Post form for the register page
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // If the form details aren't valid details then the view is returned
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByNameAsync(registerViewModel.EmailAddress);

            //If the user's email is already in use then an error is returned telling the user
            if (user != null) 
            {
                ModelState.AddModelError(string.Empty, "Email address is already in use");
                return View(registerViewModel);
            }

            //A new customer model is made using the view model passed through the parameter of the task
            var newUser = new Customer { 
                Email = registerViewModel.EmailAddress, 
                UserName = registerViewModel.EmailAddress, 
                FirstName = registerViewModel.FirstName,
                Surname = registerViewModel.Surname, 
                Title = registerViewModel.Title, 
                AddressFirstline = registerViewModel.AddressFirstline,
                AddressSecondline = registerViewModel.AddressSecondline, 
                CityTown = registerViewModel.CityTown, 
                AddressPostcode = registerViewModel.AddressPostcode,
                Gender = registerViewModel.Gender, 
                Phone = registerViewModel.Phone
            };

            //A new user is made from the view model
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!newUserResponse.Succeeded)
            {
                //If the user can't be created then an error is returned
                ModelState.AddModelError(string.Empty, "Could not create new User.");
                return View(registerViewModel);
            }
            return RedirectToAction("Login");

        }

        //Post form action for logging out 
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //Sign in manager's sign out method signs the user out
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

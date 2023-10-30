using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace RankinRetro.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Need to confirm password")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        
        //Enum of titles
        [Required]
        [Display(Name = "Title")]
        public UserTitle Title { get; set; }
        [Required]
        [Display(Name = "Address first line")]
        public string AddressFirstline { get; set; }
        [Display(Name = "Address second line")]
        public string? AddressSecondline { get; set; }
        [Required]
        [Display(Name = "City/Town")]
        public string CityTown { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string AddressPostcode { get; set; }


        //For recommendations of products
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}

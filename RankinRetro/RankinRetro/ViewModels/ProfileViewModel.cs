using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace RankinRetro.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

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

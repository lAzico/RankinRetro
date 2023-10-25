using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace RankinRetro.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email addressis required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        //Enum of titles
        public UserTitle Title { get; set; }
        [Required]
        public string AddressFirstline { get; set; }
        public string AddressSecondline { get; set; }
        [Required]
        public string CityTown { get; set; }

        [Required]
        public string AddressPostcode { get; set; }


        //For recommendations of products
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}

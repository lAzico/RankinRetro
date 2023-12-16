using RankinRetro.Data.Enum;
using RankinRetro.Models;
using System.ComponentModel.DataAnnotations;

namespace RankinRetro.ViewModels
{
    public class CartDisplayViewModel
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


        [Display(Name = "First name")]
        public string ShippingFirstName { get; set; }
        [Required]
        public string ShippingSurname { get; set; }

        //Enum of titles
        [Required]
        [Display(Name = "Title")]
        public UserTitle ShippingTitle { get; set; }
        [Required]
        [Display(Name = "Address first line")]
        public string ShippingAddressFirstline { get; set; }
        [Display(Name = "Address second line")]
        public string? ShippingAddressSecondline { get; set; }
        [Required]
        [Display(Name = "City/Town")]
        public string ShippingCityTown { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string ShippingAddressPostcode { get; set; }


        //For recommendations of products
        public ICollection<ShoppingCartDetail> CartItems { get; set; }

        public decimal DiscountAmount { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RankinRetro.Data.Enum;

namespace RankinRetro.Models
{
    public class Customer : IdentityUser
    {

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? Surname { get; set; }
        
        //Enum of titles
        public UserTitle Title { get; set; }
        [Required]
        public string? AddressFirstline { get; set; }
        public string? AddressSecondline{ get; set; }
        public string? CityTown { get; set; }

        [Required]
        public string? AddressPostcode { get; set; }


        //For recommendations of products
        public Gender Gender { get; set; }
        public string? Phone {  get; set; }




    }
}

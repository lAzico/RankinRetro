using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RankinRetro.Data.Enum;

namespace RankinRetro.Models
{
    public class Customer : IdentityUser
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }

        //Enum of titles
        public UserTitle Title { get; set; }
        [Required]
        public string Address { get; set; }

        //For recommendations of products
        public string Gender { get; set; }
        public string Phone {  get; set; }




    }
}

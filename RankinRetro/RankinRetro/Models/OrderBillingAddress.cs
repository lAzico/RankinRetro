using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class OrderBillingAddress
    {

        [Required]
        [Key]
        public int BillingAddressId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        //Enum of titles
        [Required]
        public UserTitle Title { get; set; }
        [Required]
        public string AddressFirstline { get; set; }
        public string? AddressSecondline { get; set; }
        [Required]
        public string CityTown { get; set; }

        [Required]
        public string AddressPostcode { get; set; }

    }
}

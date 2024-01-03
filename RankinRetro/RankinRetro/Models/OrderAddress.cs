using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class OrderAddress
    {
        [Required]
        [Key]
        public int ShippingAddressId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }

        public string ShippingFirstName { get; set; }
        [Required]
        public string ShippingSurname { get; set; }

        [Required]
        public UserTitle ShippingTitle { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ShippingAddressFirstline { get; set; }
        public string? ShippingAddressSecondline { get; set; }
        [Required]
        public string ShippingCityTown { get; set; }

        [Required]
        public string ShippingAddressPostcode { get; set; }


    }

}


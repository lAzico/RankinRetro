using RankinRetro.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankinRetro.Models
{
    public class ReviewAndRating
    {
        [Key]
        public int ReviewId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId {  get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Rating Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate {  get; set; }

    }
}

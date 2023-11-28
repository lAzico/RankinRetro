using System.ComponentModel.DataAnnotations;

namespace RankinRetro.Models
{
    public class AzureStorageConfig
    {
        [Key]
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ImageContainer { get; set; }
        public string ThumbnailContainer { get; set; }
    }
}

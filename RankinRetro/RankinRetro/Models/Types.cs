﻿using System.ComponentModel.DataAnnotations;

namespace RankinRetro.Models
{
    public class Types
    {

        [Key]
        public int TypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }


    }
}

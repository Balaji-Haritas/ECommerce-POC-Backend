﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommercePOC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public string ImageUrl {  get; set; }

        [Required]
        public int CategoryId {  get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}

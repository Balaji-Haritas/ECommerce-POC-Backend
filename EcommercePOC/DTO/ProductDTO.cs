﻿namespace EcommercePOC.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
       // public IFormFile Image { get; set; }
    }
}

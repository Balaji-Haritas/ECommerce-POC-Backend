using EcommercePOC.DataAccess;
using EcommercePOC.DTO;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        //private readonly IPhotoRepository _photoRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _dbContext;

        public ProductController( IProductRepository productRepository, AppDbContext dbContext)
        {
            //_photoRepository = photoRepository;
            _productRepository = productRepository;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryName(string categoryName)
        {
            var products = await _productRepository.GetProductsByCategoryNameAsync(categoryName);

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }
        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
                Quantity = productDTO.Quantity,
                ImageUrl = productDTO.ImageUrl,
            };
           

            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Product ID Mismatch");
            }
            var exisistingProduct = await _productRepository.GetProductByIdAsync(id);
            if (exisistingProduct == null)
            {
                return NotFound();
            }
            await _productRepository.UpdateProductAsync(productDTO);
            return Ok(exisistingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            await _productRepository.SaveAsync();
            return NoContent();
        }


        //[HttpPost("add-photo")]
        //public async Task<ActionResult> AddPhoto(IFormFile file)
        //{
        //    var result = await _photoRepository.AddPhotoAsync(file);

        //    if (result.Error != null)
        //    {
        //        return BadRequest(result.Error.Message);
        //    }

        //    return Ok(result.Url); 
        //}

    }

}


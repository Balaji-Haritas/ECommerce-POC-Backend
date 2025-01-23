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
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _dbContext;
        ///private readonly IWebHostEnvironment _environment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment,AppDbContext appDbContext)
        {
            _productRepository = productRepository;
            _dbContext = appDbContext;
            //_environment = webHostEnvironment;
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

        public async Task<ActionResult<Product>> PostProduct([FromForm]ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
            };
           

            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }


    }

}


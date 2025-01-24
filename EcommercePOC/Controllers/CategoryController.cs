using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository CategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await CategoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await CategoryRepository.GetCategoryByIdAsync(id);

            if (category == null) 
            {
                return NotFound();
            }
            return Ok(category);
        }

        //[HttpGet("name/{name}")]
        //public async Task<ActionResult<Category>>GetCategoryByName(string name)
        //{
        //    var category= await CategoryRepository.GetCategoryByNameAsync(name);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        [HttpPost]

        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            await CategoryRepository.AddCategoryAsync(category);
            await CategoryRepository.SaveAsync();
            return CreatedAtAction(nameof(GetCategories),new {id = category.CategoryId},category);
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int id)
        {
            var products = await CategoryRepository.GetProductsByCategoryIdAsync(id);

            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

    }
}

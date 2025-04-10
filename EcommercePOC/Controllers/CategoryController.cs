﻿using EcommercePOC.DTO;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/Category
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await CategoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Category/{id}
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

        // GET: api/Category/{id}/products
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

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            await CategoryRepository.AddCategoryAsync(category);
            await CategoryRepository.SaveAsync();
            return CreatedAtAction(nameof(GetCategories),new {id = category.CategoryId},category);
        }

        //Delete:api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await CategoryRepository.DeleteCategoryAsync(id);
            await CategoryRepository.SaveAsync();
            return NoContent();
        }

    }
}

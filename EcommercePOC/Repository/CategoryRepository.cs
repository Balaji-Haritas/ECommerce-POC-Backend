using EcommercePOC.DataAccess;
using EcommercePOC.DTO;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
                })
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        //public async Task<Category>GetCategoryByNameAsync(string name)
        //{
        //    return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
        //}

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products.Include(c => c.Category).Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(int id) 
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null) 
            {
                _dbContext.Categories.Remove(category);
            }
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}

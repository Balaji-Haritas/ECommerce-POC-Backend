﻿using EcommercePOC.DataAccess;
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

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
           return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        //public async Task<Category>GetCategoryByNameAsync(string name)
        //{
        //    return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
        //}

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}

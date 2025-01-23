using EcommercePOC.DataAccess;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.Include(p => p.Category).ToListAsync();   
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName) 
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == categoryName)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

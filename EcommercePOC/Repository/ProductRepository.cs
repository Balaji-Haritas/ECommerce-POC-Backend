using EcommercePOC.DataAccess;
using EcommercePOC.DTO;
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await (from p in _dbContext.Products
                          join c in _dbContext.Categories on p.CategoryId equals c.CategoryId
                          select new Product
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Category = c,
                              Price = p.Price,
                              Description = p.Description,
                              CategoryId = p.CategoryId,
                              Quantity = p.Quantity,
                              ImageUrl = p.ImageUrl,
                          }).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName) 
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == categoryName)
                .ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductDTO product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct != null) 
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.ImageUrl = product.ImageUrl;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

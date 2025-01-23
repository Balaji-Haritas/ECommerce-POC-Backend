using EcommercePOC.Models;

namespace EcommercePOC.RepositoryInterface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName);
        Task AddProductAsync(Product product);
        Task SaveAsync();
    }
}

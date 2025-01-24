using EcommercePOC.Models;

namespace EcommercePOC.RepositoryInterface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        //Task<Category>GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task SaveAsync();


    }
}

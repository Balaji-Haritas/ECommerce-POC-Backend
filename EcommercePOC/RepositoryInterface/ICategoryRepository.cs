using EcommercePOC.Models;

namespace EcommercePOC.RepositoryInterface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        //Task<Category>GetCategoryByNameAsync(string name);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task AddCategoryAsync(Category category);

        Task DeleteCategoryAsync(int id);

        Task SaveAsync();


    }
}

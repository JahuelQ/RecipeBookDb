using RecipeBookDb.Models;

namespace RecipeBookDb.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> CreateCategory(Category category);
        public Task DeleteCategory(int categoryId);
        public Task<List<Category>> GetAllCategories();
    }
}

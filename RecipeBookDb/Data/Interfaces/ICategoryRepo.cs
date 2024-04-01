using RecipeBookDb.Models;

namespace RecipeBookDb.Data.Interfaces
{
    public interface ICategoryRepo
    {
        public Task<Category> CreateCategory(Category category);
        public Task DeleteCategory(int categoryId);
        public Task<List<Category>> GetAllCategories();
    }
}

using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;

namespace RecipeBookDb.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        public CategoryService(ICategoryRepo repo)
        {
            _repo = repo;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            return await _repo.CreateCategory(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _repo.DeleteCategory(categoryId);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _repo.GetAllCategories();
        }
    }
}

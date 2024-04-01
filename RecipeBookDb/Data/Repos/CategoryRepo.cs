using Microsoft.EntityFrameworkCore;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;

namespace RecipeBookDb.Data.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly RecipeBookDbContext _context;
        public CategoryRepo(RecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}

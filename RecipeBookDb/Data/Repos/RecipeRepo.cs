using Microsoft.EntityFrameworkCore;
using RecipeBookDb.Data;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Data.Repos
{
    public class RecipeRepo : IRecipeRepo
    {
        // DI för DbContext som används för att anropa databasen.
        private readonly RecipeBookDbContext _context;
        public RecipeRepo(RecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculateAverageRating(int recipeId)
        {
            var ratings = await _context.Ratings
            .Where(r => r.RecipeId == recipeId)
            .ToListAsync();

            if (ratings.Count == 0) return 0;

            return ratings.Average(r => r.Score);
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task DeleteRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipe(string title)
        {
            return await _context.Recipes.FindAsync(title);
        }

        public async Task<Recipe> GetRecipeId(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<List<Recipe>> GetUserRecipes(int userId)
        {
            return await _context.Recipes
            .Where(recipe => recipe.UserId == userId)
            .ToListAsync();
        }

        public async Task<bool> UpdateRecipe(Recipe recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(recipe.RecipeId);
            if (existingRecipe == null)
            {
                return false;
            }

            _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

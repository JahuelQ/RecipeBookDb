using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Data.Interfaces
{
    public interface IRecipeRepo
    {
        public Task<Recipe> CreateRecipe(Recipe recipe);
        public Task DeleteRecipe(int recipeId);
        public Task<bool> UpdateRecipe(Recipe recipe);
        public Task<List<Recipe>> GetAllRecipes();
        public Task<Recipe> GetRecipe(string title);
        public Task<Recipe> GetRecipeId(int id);
        public Task<List<Recipe>> GetUserRecipes(int userId);
        public Task<decimal> CalculateAverageRating(int recipeId);
    }
}

using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Core.Interfaces
{
    public interface IRecipeService
    {
        public Task<RecipeDTO> CreateRecipe(RecipeDTO recipeDto);
        public Task DeleteRecipe(int recipeId);
        public Task<bool> UpdateRecipe(RecipeDTO recipeDto, int userId);
        public Task<List<Recipe>> GetAllRecipes();
        public Task<Recipe> GetRecipe(string title);
        public Task<Recipe> GetRecipeId(int id);
        public Task<List<RecipeDTO>> GetUserRecipes(int userId);
        public Task<decimal> CalculateAverageRating(int recipeId);
    }
}

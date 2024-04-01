using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repo;
        public RecipeService(IRecipeRepo repo)
        {
            _repo = repo;
        }

        public async Task<decimal> CalculateAverageRating(int recipeId)
        {
            return await _repo.CalculateAverageRating(recipeId);
        }

        /// <summary>
        /// Skapa nytt recept ///
        /// </summary>
        public async Task<RecipeDTO> CreateRecipe(RecipeDTO recipeDto)
        {
            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions,
                UserId = recipeDto.UserId,
                CategoryId = recipeDto.CategoryId
            };

            var createdRecipe = await _repo.CreateRecipe(recipe);
            if (createdRecipe == null)
            {
                return null;
            }

            return new RecipeDTO
            {
                Title = createdRecipe.Title,
                Ingredients = createdRecipe.Ingredients,
                Instructions = createdRecipe.Instructions,
                UserId = createdRecipe.UserId,
                CategoryId = createdRecipe.CategoryId
            };
        }
        /// <summary>
        /// Ta bort recept ///
        /// </summary>
        public async Task DeleteRecipe(int recipeId)
        {
            await _repo.DeleteRecipe(recipeId);
        }
        /// <summary>
        ///  Hämta alla recept ///
        /// </summary>
        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _repo.GetAllRecipes();
        }
        /// <summary>
        /// Sök på title och hämta recept ///
        /// </summary>
        public async Task<Recipe> GetRecipe(string title)
        {
            return await _repo.GetRecipe(title);
        }
        /// <summary>
        /// Hämta recept med ID ///
        /// </summary>
        public async Task<Recipe> GetRecipeId(int id)
        {
            return await _repo.GetRecipeId(id);
        }
        /// <summary>
        /// Hämta alla recept för en användare ///
        /// </summary>
        public async Task<List<RecipeDTO>> GetUserRecipes(int userId)
        {
            var recipes = await _repo.GetUserRecipes(userId);

            return recipes.Select(r => new RecipeDTO
            {
                Title = r.Title,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions,
                UserId = r.UserId,
                CategoryId = r.CategoryId
            }).ToList();
        }
        /// <summary>
        /// Uppdatera recept kopplad till User ID ///
        /// </summary>
        public async Task<bool> UpdateRecipe(RecipeDTO recipeDto, int userId)
        {
            var recipeToUpdate = await _repo.GetRecipeId(recipeDto.RecipeId);

            if (recipeToUpdate == null || recipeToUpdate.UserId != userId)
            {
                return false;
            }

            recipeToUpdate.Title = recipeDto.Title;
            recipeToUpdate.Ingredients = recipeDto.Ingredients;
            recipeToUpdate.Instructions = recipeDto.Instructions;
            recipeToUpdate.CategoryId = recipeDto.CategoryId;

            return await _repo.UpdateRecipe(recipeToUpdate);
        }

    }
}

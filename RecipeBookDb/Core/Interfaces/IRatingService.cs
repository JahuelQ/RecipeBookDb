using RecipeBookDb.Models;

namespace RecipeBookDb.Core.Interfaces
{
    public interface IRatingService
    {
        public Task<bool> AddOrUpdateRatingAsync(int userId, int recipeId, decimal score);
        public Task<decimal?> GetAverageRatingAsync(int recipeId);
    }
}

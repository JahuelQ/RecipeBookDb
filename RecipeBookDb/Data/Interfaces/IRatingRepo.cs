using RecipeBookDb.Models;

namespace RecipeBookDb.Data.Interfaces
{
    public interface IRatingRepo
    {
        public Task AddOrUpdateRatingAsync(Rating rating);
        public Task<decimal?> GetAverageRatingAsync(int recipeId);
        public Task<bool> RatingExistsAsync(int userId, int recipeId);
    }
}

using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;

namespace RecipeBookDb.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepo _ratingRepo;
        private readonly IRecipeRepo _recipeRepo;
        public RatingService(IRatingRepo repo, IRecipeRepo recipeRepo)
        {
            _ratingRepo = repo;
            _recipeRepo = recipeRepo;
        }

        public async Task<bool> AddOrUpdateRatingAsync(int userId, int recipeId, decimal score)
        {
            if (score < 1 || score > 5)
            {
                throw new ArgumentException("Rating must be 1 - 5.");
            }

            var recipe = await _recipeRepo.GetRecipeId(recipeId);
            if (recipe == null || recipe.UserId == userId)
            {
                return false;
            }

            var rating = new Rating
            {
                UserId = userId,
                RecipeId = recipeId,
                Score = score
            };

            await _ratingRepo.AddOrUpdateRatingAsync(rating);
            return true;
        }

        public async Task<decimal?> GetAverageRatingAsync(int recipeId)
        {
            return await _ratingRepo.GetAverageRatingAsync(recipeId);
        }
    }
}

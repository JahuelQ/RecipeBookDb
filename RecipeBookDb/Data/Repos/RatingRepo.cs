using Microsoft.EntityFrameworkCore;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Models;

namespace RecipeBookDb.Data.Repos
{
    public class RatingRepo : IRatingRepo
    {
        private readonly RecipeBookDbContext _context;
        public RatingRepo(RecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateRatingAsync(Rating rating)
        {
            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == rating.UserId && r.RecipeId == rating.RecipeId);

            if (existingRating != null)
            {
                existingRating.Score = rating.Score;
            }
            else
            {
                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<decimal?> GetAverageRatingAsync(int recipeId)
        {
            return await _context.Ratings
                .Where(r => r.RecipeId == recipeId)
                .AverageAsync(r => (decimal?)r.Score);
        }

        public async Task<bool> RatingExistsAsync(int userId, int recipeId)
        {
            return await _context.Ratings
                .AnyAsync(r => r.UserId == userId && r.RecipeId == recipeId);
        }
    }
}

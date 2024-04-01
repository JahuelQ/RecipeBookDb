using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Models.DTO;
using System.Security.Claims;

namespace RecipeBookDb.Controllers
{
    [Route("api/ratings/")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("addOrUpdate")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdateRating(RatingDTO ratingDto)
        {
            var token = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(token, out var userId))
            {
                return Unauthorized("Invalid token.");
            }

            try
            {
                var result = await _ratingService.AddOrUpdateRatingAsync(userId, ratingDto.RecipeId, ratingDto.Score);
                if (!result)
                {
                    return BadRequest("Could not add a score.");
                }
                return Ok("Score added/updated.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Hämtar genomsnittsbetyget för ett recept
        [HttpGet("average/{recipeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAverageRating(int recipeId)
        {
            var averageRating = await _ratingService.GetAverageRatingAsync(recipeId);
            if (averageRating == null)
            {
                return NotFound("Invalid recipe.");
            }
            return Ok(new { AverageRating = averageRating });
        }
    }
}

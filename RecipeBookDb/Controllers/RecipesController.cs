using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Models.DTO;
using System.Security.Claims;

namespace RecipeBookDb.Controllers
{
    [Route("api/recipes/")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipesController(IRecipeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> CreateRecipe(RecipeDTO recipeDto)
        {
            var token = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(token, out var userId))
            {
                return Unauthorized("Invalid token.");
            }

            recipeDto.UserId = userId;
            var newRecipeDto = await _service.CreateRecipe(recipeDto);
            if (newRecipeDto == null)
            {
                return BadRequest("Failed to create recipe.");
            }

            return Ok(newRecipeDto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-all")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _service.GetAllRecipes();
            return Ok(recipes);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get/{title}")]
        public async Task<IActionResult> GetRecipe(string title)
        {
            var recipe = await _service.GetRecipe(title);
            if (recipe == null) return NotFound("Can't find a recipe with that title.");
            return Ok(recipe);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-id/{id}")]
        public async Task<IActionResult> GetRecipeId(int id)
        {
            var recipe = await _service.GetRecipeId(id);
            if (recipe == null) return NotFound("No recipe with that ID.");
            return Ok(recipe);
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _service.GetRecipeId(id);
            if (recipe == null) return NotFound();
            await _service.DeleteRecipe(id);
            return Ok(recipe.Title + ", has been removed.");
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> UpdateRecipe(RecipeDTO recipeDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool updateResult = await _service.UpdateRecipe(recipeDto, int.Parse(userId));

            if (!updateResult)
            {
                return BadRequest("Changes to recipe, failed.");
            }

            return Ok($"{recipeDto.Title}, has been updated.");
        }



    }
}

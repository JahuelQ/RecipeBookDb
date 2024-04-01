using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Models;

namespace RecipeBookDb.Controllers
{
    [Route("api/categories/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            var newCategory = await _service.CreateCategory(category);
            if (newCategory == null)
            {
                return BadRequest("Failed to create category.");
            }

            return Ok(newCategory);
        }

        [HttpDelete]
        [Route("delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _service.DeleteCategory(categoryId);
            return Ok("Category deleted.");
        }

    }
}

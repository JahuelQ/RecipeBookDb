using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;
using System.Security.Claims;

namespace RecipeBookDb.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var newUser = await _service.CreateUser(user);
            if (newUser == null)
            {
                return BadRequest("Unable to create user.");
            }

            return Ok(newUser);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userDto = await _service.GetUser(id);
            if (userDto == null) return NotFound("No user with that ID.");

            return Ok(userDto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtos = await _service.GetAllUsers();
            if (userDtos == null || !userDtos.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(userDtos);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginDTO user)
        {
            var userDto = await _service.LoginUser(user.Username, user.Password);
            if (userDto == null)
            {
                return Unauthorized("Invalid Username or Password.");
            }

            return Ok(new { userDto.Username, userDto.Token });
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.DeleteUser(id, userId);
            if (!result) return Forbid("You are not allowed to delete this user.");
            return Ok("User has been deleted.");
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.UpdateUser(user, userId);
            if (!result) return Forbid("You are not allowed to update this user.");
            return Ok("User updated successfully.");
        }
    }
}

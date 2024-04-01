using Microsoft.AspNetCore.Http.HttpResults;
using RecipeBookDb.Core.Interfaces;
using RecipeBookDb.Data.Interfaces;
using RecipeBookDb.Data.Repos;
using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IAuthService _authService;
        private readonly IRecipeService _recipeService;
        public UserService(IUserRepo repo, IAuthService authService, IRecipeService recipeService)
        {
            _repo = repo;
            _authService = authService;
            _recipeService = recipeService;
        }

        /// <summary>
        /// Skapa ny användare ///
        /// </summary>
        public async Task<UserDTO> CreateUser(User user)
        {
            var createdUser = await _repo.CreateUser(user);
            if (createdUser == null)
            {
                return null;
            }

            return new UserDTO
            {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
            };
        }
        /// <summary>
        /// Ta bort användare ///
        /// </summary>
        public async Task<bool> DeleteUser(int id, string loggedInUserId)
        {
            var user = await _repo.GetUser(id);
            if (user == null || user.UserId.ToString() != loggedInUserId)
            {
                return false;
            }
            await _repo.DeleteUser(id);
            return true;
        }
        /// <summary>
        /// Hämta alla användare ///
        /// </summary>
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();
            if (users == null || !users.Any())
            {
                return null;
            }

            var userDtos = users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                
            }).ToList();

            return userDtos;
        }
        /// <summary>
        /// Hämta användare med ID tillsammans med skapade Recept ///
        /// </summary>
        public async Task<UserDTO> GetUser(int userId)
        {
            var user = await _repo.GetUser(userId);
            if (user == null)
            {
                return null;
            }

            var recipesDto = await _recipeService.GetUserRecipes(userId);

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Recipes = recipesDto
            };
        }
        /// <summary>
        /// Inloggning av användare ///
        /// </summary>
        public async Task<UserDTO> LoginUser(string username, string password)
        {
            var user = await _repo.GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                var token = _authService.GenerateToken(user);
                return new UserDTO { UserId = user.UserId, Username = user.Username, Token = token };
            }
            return null;
        }
        /// <summary>
        /// Uppdatera användare ///
        /// </summary>
        public async Task<bool> UpdateUser(User user, string loggedInUserId)
        {
            if (user.UserId.ToString() != loggedInUserId)
            {
                return false;
            }
            return await _repo.UpdateUser(user);
        }
    }
}

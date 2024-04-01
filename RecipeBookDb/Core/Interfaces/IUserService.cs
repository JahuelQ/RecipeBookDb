using RecipeBookDb.Models;
using RecipeBookDb.Models.DTO;

namespace RecipeBookDb.Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> LoginUser(string username, string password);
        public Task<UserDTO> CreateUser(User user);
        public Task<bool> DeleteUser(int id, string loggedInUserId);
        public Task<bool> UpdateUser(User user, string loggedInUserId);
        public Task<List<UserDTO>> GetAllUsers();
        public Task<UserDTO> GetUser(int userId);
    }
}

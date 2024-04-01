using RecipeBookDb.Models;

namespace RecipeBookDb.Data.Interfaces
{
    public interface IUserRepo
    {
        public Task<User> CreateUser(User user);
        public Task DeleteUser(int userId);
        public Task<bool> UpdateUser(User user);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUser(int userId);
        public Task<User> GetUserByUsername(string username);
    }
}

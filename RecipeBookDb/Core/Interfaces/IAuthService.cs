using RecipeBookDb.Models;

namespace RecipeBookDb.Core.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}

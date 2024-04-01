namespace RecipeBookDb.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public List<RecipeDTO> Recipes { get; set; } = new List<RecipeDTO>();
    }
}

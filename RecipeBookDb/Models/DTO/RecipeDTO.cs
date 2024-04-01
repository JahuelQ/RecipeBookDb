namespace RecipeBookDb.Models.DTO
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}

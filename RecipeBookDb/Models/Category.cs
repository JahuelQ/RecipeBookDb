using System.ComponentModel.DataAnnotations;

namespace RecipeBookDb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Recipe> recipes { get; set; } = new List<Recipe>();
    }
}

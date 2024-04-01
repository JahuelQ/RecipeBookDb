using System.ComponentModel.DataAnnotations;

namespace RecipeBookDb.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(70)]
        public string Email { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}

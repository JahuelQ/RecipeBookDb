using Microsoft.EntityFrameworkCore;
using RecipeBookDb.Models;

namespace RecipeBookDb.Data
{
    public class RecipeBookDbContext : DbContext
    {

        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Betygsskalan är mellan 0 och 5.
            modelBuilder.Entity<Rating>()
                .Property(r => r.Score)
                .HasPrecision(5, 2);

            // En user kan bara ha ett betyg per recept.
            modelBuilder.Entity<Rating>()
                .HasIndex(r => new { r.UserId, r.RecipeId })
                .IsUnique();


            // Logik för att sätta upp relationer mellan tabellerna.
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(rat => rat.Recipe)
                .WithMany(rec => rec.Ratings)
                .HasForeignKey(rat => rat.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(rat => rat.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(rat => rat.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

    
}

using GamesListApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesListApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameCategory>().HasKey(gc => new {gc.GameId, gc.CategoryId});
            modelBuilder.Entity<GameCategory>().HasOne(g => g.Game).WithMany(gc => gc.GameCategories).HasForeignKey(c => c.GameId);
            modelBuilder.Entity<GameCategory>().HasOne(c => c.Category).WithMany(gc => gc.GameCategories).HasForeignKey(g => g.CategoryId);

            modelBuilder.Entity<GameUser>().HasKey(gu => new { gu.GameId, gu.UserId });
            modelBuilder.Entity<GameUser>().HasOne(g => g.Game).WithMany(gu => gu.GameUsers).HasForeignKey(u => u.GameId);
            modelBuilder.Entity<GameUser>().HasOne(u => u.User).WithMany(gu => gu.GameUsers).HasForeignKey(g => g.UserId);
        }
    }
}

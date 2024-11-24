using GamesListApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesListApp.Data
{
    public static class DataPrep
    {
        public static void PopulateData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        private static void SeedData(DataContext context)
        {
            context.Database.Migrate();

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Action" },
                    new Category { Name = "Adventure" },
                    new Category { Name = "Strategy" },
                    new Category { Name = "Role-Playing" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed Games
            if (!context.Games.Any())
            {
                var games = new List<Game>
                {
                    new Game { Name = "The Legend of Zelda", ReleaseDate = new DateTime(1986, 2, 21) },
                    new Game { Name = "Civilization VI", ReleaseDate = new DateTime(2016, 10, 21) },
                    new Game { Name = "Dark Souls", ReleaseDate = new DateTime(2011, 9, 22) }
                };
                context.Games.AddRange(games);
                context.SaveChanges();
            }

            // Seed Users
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Name = "PlayerOne", BirthDate = new DateTime(1990, 5, 15) },
                    new User { Name = "GamerDude", BirthDate = new DateTime(1985, 8, 22) },
                    new User { Name = "StrategyMaster", BirthDate = new DateTime(1995, 11, 3) }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // Seed Reviews
            if (!context.Reviews.Any())
            {
                var reviews = new List<Review>
                {
                    new Review { Title = "Fantastic Game", Text = "I love this game!", Game = context.Games.First(g => g.Name == "The Legend of Zelda"), User = context.Users.First(u => u.Name == "PlayerOne") },
                    new Review { Title = "Great Strategy", Text = "Highly engaging and thought-provoking.", Game = context.Games.First(g => g.Name == "Civilization VI"), User = context.Users.First(u => u.Name == "GamerDude") },
                    new Review { Title = "Challenging but Fun", Text = "Dark Souls is tough but rewarding.", Game = context.Games.First(g => g.Name == "Dark Souls"), User = context.Users.First(u => u.Name == "StrategyMaster") }
                };
                context.Reviews.AddRange(reviews);
                context.SaveChanges();
            }

            // Seed GameCategories
            if (!context.GameCategories.Any())
            {
                var gameCategories = new List<GameCategory>
                {
                    new GameCategory { GameId = context.Games.First(g => g.Name == "The Legend of Zelda").Id, CategoryId = context.Categories.First(c => c.Name == "Adventure").Id },
                    new GameCategory { GameId = context.Games.First(g => g.Name == "Civilization VI").Id, CategoryId = context.Categories.First(c => c.Name == "Strategy").Id },
                    new GameCategory { GameId = context.Games.First(g => g.Name == "Dark Souls").Id, CategoryId = context.Categories.First(c => c.Name == "Role-Playing").Id }
                };
                context.GameCategories.AddRange(gameCategories);
                context.SaveChanges();
            }

            // Seed GameUsers
            if (!context.GameUsers.Any())
            {
                var gameUsers = new List<GameUser>
                {
                    new GameUser { GameId = context.Games.First(g => g.Name == "The Legend of Zelda").Id, UserId = context.Users.First(u => u.Name == "PlayerOne").Id },
                    new GameUser { GameId = context.Games.First(g => g.Name == "Civilization VI").Id, UserId = context.Users.First(u => u.Name == "GamerDude").Id },
                    new GameUser { GameId = context.Games.First(g => g.Name == "Dark Souls").Id, UserId = context.Users.First(u => u.Name == "StrategyMaster").Id }
                };
                context.GameUsers.AddRange(gameUsers);
                context.SaveChanges();
            }
        }

    }
}

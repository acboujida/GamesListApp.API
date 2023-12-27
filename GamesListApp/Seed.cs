/*using GamesListApp.Data;
using GamesListApp.Models;
using System.Diagnostics.Metrics;

namespace GamesListApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.GameUsers.Any())
            {
                var gameUsers = new List<GameUser>()
                {
                    new GameUser()
                    {
                        Game = new Game()
                        {
                            Name = "Nioh",
                            ReleaseDate = new DateTime(2017, 11, 7),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Action"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Best Souls-like",Text = "I love this game",
                                User = new User(){ Name = "Marc", BirthDate = new DateTime(1992, 11, 7)} },
                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks",
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        User = new User()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new GameUser()
                    {
                        Game = new Game()
                        {
                            Name = "Squirtle",
                            BirthDate = new DateTime(1903,1,1),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best Game, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        User = new User()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                                    new GameUser()
                    {
                        Game = new Game()
                        {
                            Name = "Venasuar",
                            BirthDate = new DateTime(1903,1,1),
                            GameCategories = new List<GameCategory>()
                            {
                                new GameCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best Game, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        User = new User()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.GameUsers.AddRange(GameUsers);
                dataContext.SaveChanges();
            }
        }

    }
}
*/
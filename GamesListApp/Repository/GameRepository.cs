using GamesListApp.Data;
using GamesListApp.Interfaces;
using GamesListApp.Models;

namespace GamesListApp.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }

        public Game GetGame(int id)
        {
            return _context.Games.Where(g => g.Id == id).FirstOrDefault();
        }

        public Game GetGame(string name)
        {
            return _context.Games.Where(g => g.Name == name).FirstOrDefault();
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Id).ToList();
        }

        public ICollection<Category> GetCategoriesOfGame(int id)
        {
            return _context.GameCategories.Where(gc => gc.GameId == id).Select(gc => gc.Category).ToList();
        }

        public ICollection<Review> GetReviewsOfGame(int id)
        {
            return _context.Reviews.Where(r => r.Game.Id == id).ToList();
        }

        public bool CreateGame(Game game)
        {
            _context.Add(game);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool DeleteGame(Game game)
        {
            _context.Remove(game);
            return Save();
        }
    }
}

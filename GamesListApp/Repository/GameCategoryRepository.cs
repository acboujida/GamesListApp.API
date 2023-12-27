using GamesListApp.Data;
using GamesListApp.Interfaces;
using GamesListApp.Models;

namespace GamesListApp.Repository
{
    public class GameCategoryRepository : IGameCategoryRepository
    {
        private readonly DataContext _context;

        public GameCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateGameCategory(GameCategory gamecategory)
        {
            _context.Add(gamecategory);
            return Save();
        }

        public bool DeleteGameCategory(GameCategory gamecategory)
        {
            _context.Remove(gamecategory);
            return Save();
        }

        public bool GameCategoryExists(int gameid, int categoryid)
        {
            return _context.GameCategories.Any(gc => gc.GameId == gameid && gc.CategoryId == categoryid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

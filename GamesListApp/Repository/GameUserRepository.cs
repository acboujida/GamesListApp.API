using GamesListApp.Data;
using GamesListApp.Interfaces;
using GamesListApp.Models;

namespace GamesListApp.Repository
{
    public class GameUserRepository : IGameUserRepository
    {
        private readonly DataContext _context;

        public GameUserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGameUser(GameUser gameuser)
        {
            _context.Add(gameuser);
            return Save();
        }

        public ICollection<Game> GetGamesOfUser(int userid)
        {
            return _context.GameUsers.Where(gu => gu.UserId == userid).Select(gu => gu.Game).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateGameUser(GameUser gameuser)
        {
            _context.Update(gameuser);
            return Save();
        }

        public bool GameUserExists(int userid, int gameid)
        {
            return _context.GameUsers.Any(gu => gu.UserId == userid && gu.GameId == gameid);
        }

        public bool DeleteGameUser(GameUser gameuser)
        {
            _context.Remove(gameuser);
            return Save();
        }
    }
}

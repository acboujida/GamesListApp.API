using GamesListApp.Models;

namespace GamesListApp.Interfaces
{
    public interface IGameUserRepository
    {
        ICollection<Game> GetGamesOfUser(int userid);
        bool GameUserExists(int userid, int gameid);
        bool CreateGameUser(GameUser gameuser);
        bool UpdateGameUser(GameUser gameuser);
        bool DeleteGameUser(GameUser gameuser);
        bool Save();
    }
}

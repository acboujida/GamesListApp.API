using GamesListApp.Models;

namespace GamesListApp.Interfaces
{
    public interface IGameCategoryRepository
    {
        bool GameCategoryExists(int gameid, int categoryid);
        bool CreateGameCategory(GameCategory gamecategory);
        bool Save();
        bool DeleteGameCategory(GameCategory gamecategory);
    }
}

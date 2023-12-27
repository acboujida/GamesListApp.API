using GamesListApp.Models;

namespace GamesListApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        Game GetGame(int id);
        Game GetGame(string name);
        bool GameExists(int id);
        ICollection<Category> GetCategoriesOfGame(int id);
        ICollection<Review> GetReviewsOfGame(int id);
        bool CreateGame(Game game);
        bool Save();
        bool DeleteGame(Game game);
    }
}

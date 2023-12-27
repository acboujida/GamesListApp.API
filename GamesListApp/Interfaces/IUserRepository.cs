using GamesListApp.Models;

namespace GamesListApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string name);
        bool UserExists(int id);
        ICollection<Review> GetReviewsOfUser(int id);
        ICollection<Game> GetGamesOfUser(int id);
        bool CreateUser(User user);
        bool Save();
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}

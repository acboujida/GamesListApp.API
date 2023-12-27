namespace GamesListApp.Models
{
    public class GameUser
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }
    }
}

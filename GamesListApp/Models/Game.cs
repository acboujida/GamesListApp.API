namespace GamesListApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<GameCategory> GameCategories { get; set; }
        public ICollection<GameUser> GameUsers { get; set; }
    }
}

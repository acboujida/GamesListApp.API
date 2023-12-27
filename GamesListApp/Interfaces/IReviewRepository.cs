using GamesListApp.Models;

namespace GamesListApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        User GetUserOfReview(int id);
        Game GetGameOfReview(int id);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool Save();
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
    }
}

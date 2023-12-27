using GamesListApp.Data;
using GamesListApp.Interfaces;
using GamesListApp.Models;

namespace GamesListApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }
        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(review => review.Id == id);
        }

        public Game GetGameOfReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).Select(r => r.Game).FirstOrDefault();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public User GetUserOfReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).Select(r => r.User).FirstOrDefault();
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }
    }
}

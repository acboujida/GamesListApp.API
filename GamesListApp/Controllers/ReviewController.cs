using AutoMapper;
using GamesListApp.DTO;
using GamesListApp.Interfaces;
using GamesListApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) BadRequest(ModelState);

            return Ok(reviews);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(id));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(review);
        }
        [HttpGet("{id}/user")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserOfReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            var user = _mapper.Map<UserDTO>(_reviewRepository.GetUserOfReview(id));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(user);
        }
        [HttpGet("{id}/game")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGameOfReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            var game = _mapper.Map<GameDTO>(_reviewRepository.GetGameOfReview(id));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(game);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateReview([FromBody] ReviewDTO reviewdto, [FromQuery] int userid, [FromQuery] int gameid)
        {
            if (reviewdto == null) return BadRequest(ModelState);

            var exists = _reviewRepository.GetReviews().Any(r => r.Title.Trim().ToUpper() == reviewdto.Title.Trim().ToUpper());
            if (exists)
            {
                ModelState.AddModelError("", "Review already exists!");
                return StatusCode(409);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewmodel = _mapper.Map<Review>(reviewdto);

            if (!_userRepository.UserExists(userid) || !_gameRepository.GameExists(gameid)) return BadRequest(ModelState);

            reviewmodel.User = _userRepository.GetUser(userid);
            reviewmodel.Game = _gameRepository.GetGame(gameid);

            if (!_reviewRepository.CreateReview(reviewmodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500);
            }
            return Ok("Successfully created!");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateReview(int id, [FromBody] ReviewDTO reviewdto)
        {
            if (reviewdto == null || id != reviewdto.Id) return BadRequest(ModelState);
            if (!_reviewRepository.ReviewExists(reviewdto.Id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviewmodel = _mapper.Map<Review>(reviewdto);
            if (!_reviewRepository.UpdateReview(reviewmodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteReview(int id)
        {
            if (id == 0) return BadRequest(ModelState);
            if (!_reviewRepository.ReviewExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            var review = _reviewRepository.GetReview(id);
            if (!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

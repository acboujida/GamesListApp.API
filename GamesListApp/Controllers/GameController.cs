using AutoMapper;
using GamesListApp.DTO;
using GamesListApp.Interfaces;
using GamesListApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGames()
        {
            var games = _mapper.Map<List<GameDTO>>(_gameRepository.GetGames());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(games);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGame(int id)
        {
            if (!_gameRepository.GameExists(id)) return NotFound();

            var game = _mapper.Map<GameDTO>(_gameRepository.GetGame(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(game);
        }
        [HttpGet("{id}/categories")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoriesOfGame(int id)
        {
            if (!_gameRepository.GameExists(id)) return NotFound();
            
            var categories = _mapper.Map<List<CategoryDTO>>(_gameRepository.GetCategoriesOfGame(id));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(categories);
        }
        [HttpGet("{id}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfGame(int id)
        {
            if (!_gameRepository.GameExists(id)) return NotFound();

            var reviews = _mapper.Map<List<ReviewDTO>>(_gameRepository.GetReviewsOfGame(id));

            if (!ModelState.IsValid) return BadRequest();

            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateGame([FromBody] GameDTO gamedto)
        {
            if (gamedto == null) return BadRequest(ModelState);

            var exists = _gameRepository.GetGames().Any(g => g.Name.Trim().ToUpper() == gamedto.Name.Trim().ToUpper());
            if (exists)
            {
                ModelState.AddModelError("", "Game already exists!");
                return StatusCode(409);
            }
            var gamemodel = _mapper.Map<Game>(gamedto);
            if (!_gameRepository.CreateGame(gamemodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok("Successfully created!");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteGame(int id)
        {
            if (id == 0) return BadRequest(ModelState);
            if (!_gameRepository.GameExists(id)) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var game = _gameRepository.GetGame(id);
            var reviews = _gameRepository.GetReviewsOfGame(id);
            if (reviews != null && reviews.Any())
            {
                if (!_reviewRepository.DeleteReviews(reviews.ToList()))
                {
                    ModelState.AddModelError("", "Saving failed!");
                    return StatusCode(500, ModelState);
                }
            }
            if (!_gameRepository.DeleteGame(game))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

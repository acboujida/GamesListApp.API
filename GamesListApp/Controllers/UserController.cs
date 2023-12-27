using AutoMapper;
using GamesListApp.DTO;
using GamesListApp.Interfaces;
using GamesListApp.Models;
using GamesListApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamesListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id)) return NotFound();

            var user = _mapper.Map<UserDTO>(_userRepository.GetUser(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(user);
        }
        [HttpGet("{id}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfUser(int id)
        {
            if (!_userRepository.UserExists(id)) return NotFound();

            var reviews = _mapper.Map<List<ReviewDTO>>(_userRepository.GetReviewsOfUser(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }
        [HttpGet("{id}/games")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesOfUser(int id)
        {
            if (!_userRepository.UserExists(id)) return NotFound();

            var games = _mapper.Map<List<GameDTO>>(_userRepository.GetGamesOfUser(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(games);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] UserDTO userdto)
        {
            if (userdto == null) return BadRequest(ModelState);

            var exists = _userRepository.GetUsers().Any(u => u.Name.Trim().ToUpper() == userdto.Name.Trim().ToUpper());
            if (exists)
            {
                ModelState.AddModelError("", "User already exists!");
                return StatusCode(409);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var usermodel = _mapper.Map<User>(userdto);
            if (!_userRepository.CreateUser(usermodel))
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
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userdto)
        {
            if (userdto == null || userdto.Id != id) return BadRequest(ModelState);

            if (!_userRepository.UserExists(userdto.Id)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usermodel = _mapper.Map<User>(userdto);

            if (!_userRepository.UpdateUser(usermodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteUser(int id)
        {
            if (!ModelState.IsValid || !_userRepository.UserExists(id)) return BadRequest(ModelState);

            var user = _userRepository.GetUser(id);
            var reviews = _userRepository.GetReviewsOfUser(id);

            if (reviews != null && reviews.Any())
            {
                if (!_reviewRepository.DeleteReviews(reviews.ToList()))
                {
                    ModelState.AddModelError("", "Saving failed!");
                    return StatusCode(500, ModelState);
                }
            }
            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

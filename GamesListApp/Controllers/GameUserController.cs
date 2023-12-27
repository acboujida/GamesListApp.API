using GamesListApp.Interfaces;
using GamesListApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesListApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameUserController : Controller
    {
        private readonly IGameUserRepository _gameUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public GameUserController(IGameUserRepository gameUserRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _gameUserRepository = gameUserRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        [HttpPost("User/{userid}/games/{gameid}")]
        [ProducesResponseType(200)]
        public IActionResult CreateGameUser(int userid, int gameid)
        {
            if (userid == 0 || gameid == 0) return BadRequest();
            if (!_gameRepository.GameExists(gameid) || !_userRepository.UserExists(userid)) return NotFound();

            if (_gameUserRepository.GameUserExists(userid, gameid)) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gameuser = new GameUser { GameId = gameid, UserId = userid };

            if (!_gameUserRepository.CreateGameUser(gameuser))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }

        [HttpDelete("User/{userid}/games/{gameid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteGameUser(int userid, int gameid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_gameRepository.GameExists(gameid) || !_userRepository.UserExists(userid)) return NotFound();
            if (!_gameUserRepository.GameUserExists(userid, gameid)) return BadRequest(ModelState);
            var gameuser = new GameUser { GameId = gameid, UserId = userid };
            if (!_gameUserRepository.DeleteGameUser(gameuser))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

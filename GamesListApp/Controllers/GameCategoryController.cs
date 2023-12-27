using GamesListApp.Interfaces;
using GamesListApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesListApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameCategoryController : Controller
    {
        private readonly IGameCategoryRepository _gameCategoryRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GameCategoryController(IGameCategoryRepository gameCategoryRepository, IGameRepository gameRepository, ICategoryRepository categoryRepository)
        {
            _gameCategoryRepository = gameCategoryRepository;
            _gameRepository = gameRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost("Game/{gameid}/categories/{categoryid}")]
        [ProducesResponseType(200)]
        public IActionResult UpdateGameCategory(int gameid, int categoryid)
        {
            if (gameid == 0 || categoryid == 0) return BadRequest(ModelState);
            if (_gameCategoryRepository.GameCategoryExists(gameid, categoryid)) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_gameRepository.GameExists(gameid) || !_categoryRepository.CategoryExists(categoryid)) return NotFound();
            var gamecategory = new GameCategory { CategoryId = categoryid, GameId = gameid };

            if (!_gameCategoryRepository.CreateGameCategory(gamecategory))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }
        [HttpDelete("Game/{gameid}/categories/{categoryid}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteGameCategory(int gameid, int categoryid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_gameRepository.GameExists(gameid) || !_categoryRepository.CategoryExists(categoryid)) return NotFound();
            if (!_gameCategoryRepository.GameCategoryExists(gameid, categoryid)) return BadRequest(ModelState);
            var gamecategory = new GameCategory { CategoryId = categoryid, GameId = gameid };

            if (!_gameCategoryRepository.DeleteGameCategory(gamecategory))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

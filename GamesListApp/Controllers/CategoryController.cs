using AutoMapper;
using GamesListApp.DTO;
using GamesListApp.Interfaces;
using GamesListApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace GamesListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(categories);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id)) return NotFound();

            var category = _mapper.Map<CategoryDTO>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(category);
        }
        [HttpGet("{id}/games")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesByCategory(int id) 
        {
            if (!_categoryRepository.CategoryExists(id)) return NotFound();

            var games = _mapper.Map<List<GameDTO>>(_categoryRepository.GetGamesByCategory(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(games);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody] CategoryDTO categorydto)
        {
            if (categorydto == null) return BadRequest(ModelState);
            var exists = _categoryRepository.GetCategories().Any(c => c.Name.Trim().ToUpper() == categorydto.Name.Trim().ToUpper());
            if (exists)
            {
                ModelState.AddModelError("", "Category already exists!");
                return StatusCode(409, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categorymodel = _mapper.Map<Category>(categorydto);

            if (!_categoryRepository.CreateCategory(categorymodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categorydto)
        {
            if (categorydto == null || categorydto.Id != id) return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categorydto.Id)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categorymodel = _mapper.Map<Category>(categorydto);

            if (!_categoryRepository.UpdateCategory(categorymodel))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0) return BadRequest(ModelState);
            if (!_categoryRepository.CategoryExists(id)) return NotFound();
            var category = _categoryRepository.GetCategory(id);
            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Saving failed!");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetCategoryDTO>))]
        public IActionResult GetCategories()
        {
            ICollection<GetCategoryDTO> categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(GetCategoryDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoriesExists(categoryId))
                return NotFound();

            GetCategoryDTO category = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{categoryId}/Pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetPokemonDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonsByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoriesExists(categoryId))
                return NotFound();

            ICollection<GetPokemonDTO> pokemons = _categoryRepository.GetPokemonsByCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateCategoryDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest();

            bool categoryExists = _categoryRepository.GetCategories()
                .Any(c => c.Name.Trim().ToUpper() == categoryDTO.Name.Trim().ToUpper());

            if (categoryExists)
            {
                ModelState.AddModelError("name", "Ops! Category already registered.");
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CreateCategory(categoryDTO))
            {
                ModelState.AddModelError("", "Something gets wrong while creating... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Category Successfuly created.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PokemonController(IPokemonRepository pokemonRepository,
            IOwnerRepository ownerRepository,
            ICategoryRepository categoryRepository)
        {
            _pokemonRepository = pokemonRepository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetPokemonDTO>))]
        public IActionResult GetPokemons()
        {
            ICollection<GetPokemonDTO> pokemons = _pokemonRepository.GetPokemons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(GetPokemonDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            GetPokemonDTO pokemon = _pokemonRepository.GetPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/Rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            decimal pokemonRating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemonRating);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreatePokemonDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon(CreatePokemonDTO pokemonDTO)
        {
            if (!_ownerRepository.OwnerExists(pokemonDTO.OwnerID))
            {
                ModelState.AddModelError("ownerID", "Owner not found. Verify and try again.");
                return BadRequest(ModelState);
            };

            if (!_categoryRepository.CategoriesExists(pokemonDTO.CategoryID))
            {
                ModelState.AddModelError("ownerID", "Category not found. Verify and try again.");
                return BadRequest(ModelState);
            };

            if (pokemonDTO == null)
                return BadRequest(ModelState);

            bool pokemonExists = _pokemonRepository.GetPokemons()
                .Any(p => p.Name?.Trim().ToUpper() == pokemonDTO.Name?.Trim().ToUpper());

            if (pokemonExists)
            {
                ModelState.AddModelError("Name", "Ops! Pokemon already registered.");
                return BadRequest(ModelState);
            }

            if (!_pokemonRepository.CreatePokemon(pokemonDTO))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Pokemon Successfuly created.");
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonID)
        {
            if (!_pokemonRepository.PokemonExists(pokemonID))
                return NotFound();

            if (!_pokemonRepository.DeletePokemon(pokemonID))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

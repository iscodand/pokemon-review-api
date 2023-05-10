using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using System.Diagnostics;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetOwnerDTO>))]
        public IActionResult GetOwners()
        {
            ICollection<GetOwnerDTO> owners = _ownerRepository.GetOwners();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(GetOwnerDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            GetOwnerDTO owner = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/Pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetPokemonDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            ICollection<GetPokemonDTO> pokemons = _ownerRepository.GetPokemonsByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateOwnerDTO))]
        [ProducesResponseType(404)]
        public IActionResult CreateOwner([FromBody] CreateOwnerDTO ownerDTO)
        {
            if (!_countryRepository.CountryExists(ownerDTO.CountryID))
            {
                ModelState.AddModelError("countryId", "Country not found. Verify and try again");
                return BadRequest(ModelState);
            }

            if (ownerDTO == null)
                return BadRequest(ModelState);

            if (!_ownerRepository.CreateOwner(ownerDTO))
            {
                ModelState.AddModelError("", "Something gets wrong... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Owner Successfuly Created.");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] UpdateOwnerDTO ownerDTO)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!_ownerRepository.UpdateOwner(ownerId, ownerDTO))
            {
                ModelState.AddModelError("", "Something gets wrong... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Owner Successfuly updated.");
        }


        [HttpPatch("{ownerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult PartialUpdateOwner(int ownerId, [FromBody] JsonPatchDocument<UpdateOwnerDTO> patchDocument)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ownerRepository.PartialUpdateOwner(ownerId, patchDocument))
            {
                ModelState.AddModelError("", "Something gets wrong... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Owner Successfuly updated.");
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!_ownerRepository.DeleteOwner(ownerId))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

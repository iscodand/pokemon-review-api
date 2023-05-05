using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IOwnerRepository _ownerRepository;

        public CountryController(ICountryRepository countryRepository, IOwnerRepository ownerRepository)
        {
            _countryRepository = countryRepository;
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetCountryDTO>))]
        public IActionResult GetCountries()
        {
            ICollection<GetCountryDTO> countries = _countryRepository.GetCountries();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(GetCountryDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            GetCountryDTO country = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("Owner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(GetCountryDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            GetCountryDTO country = _countryRepository.GetCountryByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("{countryId}/Owners")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetOwnerDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            ICollection<GetOwnerDTO> owners = _countryRepository.GetOwnersByCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateCountryDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (countryDTO == null)
                return BadRequest(ModelState);

            bool countryExists = _countryRepository.GetCountries()
                .Any(c => c.Name?.Trim().ToUpper() == countryDTO.Name?.Trim().ToUpper());

            if (countryExists)
            {
                ModelState.AddModelError("Name", "Ops! Category already registered.");
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CreateCountry(countryDTO))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Country Successfuly created.");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CreateCountryDTO countryDTO)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            if (!_countryRepository.UpdateCountry(countryId, countryDTO))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Country Successfuly updated.");
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            if (!_countryRepository.DeleteCountry(countryId))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

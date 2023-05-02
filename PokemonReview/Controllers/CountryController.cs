using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<GetCountryDTO>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(404)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<GetCountryDTO>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("Owner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(404)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            // Implements owner exists method.

            var country = _mapper.Map<GetCountryDTO>(
                _countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("{countryId}/Owners")]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var owners = _mapper.Map<List<Owner>>(
                _countryRepository.GetOwnersByCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }
    }
}

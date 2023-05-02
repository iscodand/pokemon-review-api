using AutoMapper;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<GetCountryDTO> GetCountries()
        {
            return _mapper.Map<List<GetCountryDTO>>(
                _context.Countries.OrderBy(c => c.Id).ToList());
        }

        public GetCountryDTO GetCountry(int countryId)
        {
            return _mapper.Map<GetCountryDTO>(
                _context.Countries.Where(c => c.Id == countryId)
                .FirstOrDefault());
        }

        public GetCountryDTO GetCountryByOwner(int ownerId)
        {
            return _mapper.Map<GetCountryDTO>(
                _context.Owners.Where(o => o.Id == ownerId)
                .Select(o => o.Country)
                .FirstOrDefault());
        }

        public ICollection<GetOwnerDTO> GetOwnersByCountry(int countryId)
        {
            return _mapper.Map<List<GetOwnerDTO>>(
                _context.Owners.Where(o => o.Country.Id == countryId).ToList());
        }

        public bool CountryExists(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }
    }
}

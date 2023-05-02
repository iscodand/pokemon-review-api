using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<GetCountryDTO> GetCountries();
        GetCountryDTO GetCountry(int countryId);
        GetCountryDTO GetCountryByOwner(int ownerId);
        ICollection<GetOwnerDTO> GetOwnersByCountry(int countryId);
        bool CountryExists(int countryId);
    }
}

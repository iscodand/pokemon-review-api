using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<GetCountryDTO> GetCountries();
        GetCountryDTO GetCountry(int countryId);
        GetCountryDTO GetCountryByOwner(int ownerId);
        ICollection<GetOwnerDTO> GetOwnersByCountry(int countryId);
        bool CountryExists(int countryId);

        bool CreateCountry(CreateCountryDTO countryDTO);
        bool DeleteCountry(int countryId);
        bool Save();
    }
}

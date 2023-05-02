using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<GetOwnerDTO> GetOwners();
        GetOwnerDTO GetOwner(int ownerId);
        ICollection<GetPokemonDTO> GetPokemonsByOwner(int ownerId);
        bool OwnerExists(int ownerId);
    }
}

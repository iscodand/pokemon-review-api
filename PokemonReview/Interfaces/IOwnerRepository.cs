using Microsoft.AspNetCore.JsonPatch;
using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<GetOwnerDTO> GetOwners();
        GetOwnerDTO GetOwner(int ownerId);
        ICollection<GetPokemonDTO> GetPokemonsByOwner(int ownerId);
        bool OwnerExists(int ownerId);

        bool CreateOwner(CreateOwnerDTO ownerDTO);
        bool UpdateOwner(int ownerId, UpdateOwnerDTO patchDocument);
        bool PartialUpdateOwner(int ownerId, JsonPatchDocument patchDocument);
        bool DeleteOwner(int ownerId);
        bool Save();
    }
}

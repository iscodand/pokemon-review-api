using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<GetPokemonDTO> GetPokemons();
        public GetPokemonDTO GetPokemon(int pokeId);
        public decimal GetPokemonRating(int pokeId);
        public bool PokemonExists(int pokeId);

        public bool CreatePokemon(CreatePokemonDTO pokemonDTO);
        public bool Save();
    }
}

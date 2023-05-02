using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons();
        public Pokemon GetPokemon(int pokeId);
        public Pokemon GetPokemon(string name);
        public decimal GetPokemonRating(int pokeId);
        public bool PokemonExists(int pokeId);
    }
}

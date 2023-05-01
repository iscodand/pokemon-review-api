using PokemonReview.Data;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int pokeId)
        {
            return _context.Pokemons.Where(p => p.Id == pokeId).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            int totalReviews = reviews.Count();

            if (totalReviews <= 0)
                return 0;

            decimal rating = ((decimal)reviews.Sum(r => r.Rating)) / totalReviews;

            return Math.Round(rating, 1);
        }

        public bool GetPokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }
    }
}

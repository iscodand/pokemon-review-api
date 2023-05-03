using AutoMapper;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PokemonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<GetPokemonDTO> GetPokemons()
        {
            return _mapper.Map<List<GetPokemonDTO>>
                (_context.Pokemons.OrderBy(p => p.Id).ToList());
        }

        public GetPokemonDTO GetPokemon(int pokeId)
        {
            return _mapper.Map<GetPokemonDTO>(
                _context.Pokemons.Where(p => p.Id == pokeId)
                .FirstOrDefault());
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

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }
    }
}

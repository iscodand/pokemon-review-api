﻿using AutoMapper;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

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
                _context.Pokemons.FirstOrDefault(p => p.Id == pokeId));
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

        public bool CreatePokemon(CreatePokemonDTO pokemonDTO)
        {
            Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
            Owner pokemonOwnerEntity = _context.Owners.First(o => o.Id == pokemonDTO.OwnerID);
            Category pokemonCategoryEntity = _context.Categories.First(c => c.Id == pokemonDTO.CategoryID);

            PokemonOwner pokemonOwner = new()
            {
                Pokemon = pokemon,
                Owner = pokemonOwnerEntity,
            };

            PokemonCategory pokemonCategory = new()
            {
                Pokemon = pokemon,
                Category = pokemonCategoryEntity,
            };

            _context.Pokemons.Add(pokemon);
            _context.PokemonOwners.Add(pokemonOwner);
            _context.PokemonCategories.Add(pokemonCategory);

            return Save();
        }
        public bool UpdatePokemon(int pokeId, UpdatePokemonDTO pokemonDTO)
        {
            Pokemon pokemon = _context.Pokemons.First(p => p.Id == pokeId);
            _mapper.Map(pokemonDTO, pokemon);
            return Save();
        }

        public bool DeletePokemon(int pokemonId)
        {
            Pokemon? pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
                return Save();
            }

            return false;
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

using AutoMapper;
using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDTO>();
        }
    }
}

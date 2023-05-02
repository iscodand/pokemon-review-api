using AutoMapper;
using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, GetPokemonDTO>();
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<Country, GetCountryDTO>();
            CreateMap<Owner, GetOwnerDTO>();
            CreateMap<Review, GetReviewDTO>();
            CreateMap<Reviewer, GetReviewerDTO>();
        }
    }
}

using AutoMapper;
using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<Country, GetCountryDTO>();
            CreateMap<CreateCountryDTO, Country>();

            CreateMap<Owner, GetOwnerDTO>();
            CreateMap<CreateOwnerDTO, Owner>();

            CreateMap<Pokemon, GetPokemonDTO>();
            CreateMap<Review, GetReviewDTO>();
            CreateMap<Reviewer, GetReviewerDTO>();
        }
    }
}

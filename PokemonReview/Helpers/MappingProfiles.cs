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

            CreateMap<Pokemon, GetPokemonDTO>();
            CreateMap<Country, GetCountryDTO>();
            CreateMap<Owner, GetOwnerDTO>();
            CreateMap<Review, GetReviewDTO>();
            CreateMap<Reviewer, GetReviewerDTO>();
        }
    }
}

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
            CreateMap<UpdateOwnerDTO, Owner>();
            CreateMap<Owner, UpdateOwnerDTO>();

            CreateMap<Pokemon, GetPokemonDTO>();
            CreateMap<CreatePokemonDTO, Pokemon>();
            CreateMap<UpdatePokemonDTO, Pokemon>();
            CreateMap<Pokemon, UpdatePokemonDTO>();

            CreateMap<Review, GetReviewDTO>();
            CreateMap<CreateReviewDTO, Review>();

            CreateMap<Reviewer, GetReviewerDTO>();
            CreateMap<CreateReviewerDTO, Reviewer>();
        }
    }
}

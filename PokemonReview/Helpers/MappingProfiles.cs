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
            CreateMap<Owner, UpdateOwnerDTO>();
            CreateMap<UpdateOwnerDTO, Owner>();

            CreateMap<Pokemon, GetPokemonDTO>();
            CreateMap<CreatePokemonDTO, Pokemon>();
            CreateMap<Pokemon, UpdatePokemonDTO>();
            CreateMap<UpdatePokemonDTO, Pokemon>();

            CreateMap<Review, GetReviewDTO>();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<Review, UpdateReviewDTO>();
            CreateMap<UpdateReviewDTO, Review>();

            CreateMap<Reviewer, GetReviewerDTO>();
            CreateMap<CreateReviewerDTO, Reviewer>();
            CreateMap<Reviewer, UpdateReviewerDTO>();
            CreateMap<UpdateReviewerDTO, Reviewer>();
        }
    }
}

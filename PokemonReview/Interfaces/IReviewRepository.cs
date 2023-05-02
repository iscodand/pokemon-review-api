using PokemonReview.Data.DTOs;
using PokemonReview.Models;

namespace PokemonReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<GetReviewDTO> GetReviews();
        GetReviewDTO GetReview(int reviewId);
        ICollection<GetReviewDTO> GetReviewsByPokemon(int pokeId);
        ICollection<GetReviewDTO> GetReviewsByReviewer(int reviewerId);
        bool ReviewExists(int reviewId);
    }
}

using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<GetReviewDTO> GetReviews();
        GetReviewDTO GetReview(int reviewId);
        ICollection<GetReviewDTO> GetReviewsByPokemon(int pokeId);
        ICollection<GetReviewDTO> GetReviewsByReviewer(int reviewerId);
        bool ReviewExists(int reviewId);
        bool DuplicatedReview(int reviewerId, int pokeId);

        bool CreateReview(CreateReviewDTO reviewDTO);
        bool Save();
    }
}

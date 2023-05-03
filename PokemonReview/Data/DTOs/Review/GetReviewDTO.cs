using PokemonReview.Models;

namespace PokemonReview.Data.DTOs
{
    public class GetReviewDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public decimal Rating { get; set; }
        public GetPokemonDTO? Pokemon { get; set; }
        public GetReviewerDTO? Reviewer { get; set; }
    }
}

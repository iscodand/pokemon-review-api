using PokemonReview.Models;

namespace PokemonReview.Data.DTOs
{
    public class GetReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public GetPokemonDTO Pokemon { get; set; }
        public Reviewer Reviewer { get; set; }
    }
}

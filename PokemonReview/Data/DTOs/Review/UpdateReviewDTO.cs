namespace PokemonReview.Data.DTOs
{
    public class UpdateReviewDTO
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public decimal Rating { get; set; }
    }
}

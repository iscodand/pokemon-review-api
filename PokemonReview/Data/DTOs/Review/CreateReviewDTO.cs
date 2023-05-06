using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    public class CreateReviewDTO
    {
        [Required(ErrorMessage = "Review needs a Pokemon!")]
        public int PokemonID { get; set; }

        [Required(ErrorMessage = "Review needs a Reviewer!")]
        public int ReviewerID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Review Title must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Review Title must contain less than 100 characters.")]
        public string? Title { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Review Text must contain at least 15 chars.")]
        [MaxLength(500, ErrorMessage = "Review Text must contain less than 500 characters.")]
        public string? Text { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be higher than 0 or less than 5.")]
        public decimal Rating { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    public class CreateReviewerDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Reviewer First Name must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Reviewer First Name must contain less than 100 characters.")]
        public string? FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Reviewer Last Name must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Reviewer Last Name must contain less than 100 characters.")]
        public string? LastName { get; set; }
    }
}

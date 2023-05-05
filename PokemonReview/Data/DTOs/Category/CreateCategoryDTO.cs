using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    // Create and Update => DRY
    public class CreateCategoryDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Category must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Category must contain less than 100 characters.")]
        public string? Name { get; set; }
    }
}

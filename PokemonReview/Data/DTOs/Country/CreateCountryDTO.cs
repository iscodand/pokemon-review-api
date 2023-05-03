using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    public class CreateCountryDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Country must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Country must contain less than 100 characters.")]
        public string? Name { get; set; }
    }
}

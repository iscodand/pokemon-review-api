using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    public class CreatePokemonDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Pokemon Name must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Pokemon Name must contain less than 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Pokemon needs an Owner!")]
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "Pokemon needs a Birth date.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Poke Category is required.")]
        public int CategoryID { get; set; }
    }
}

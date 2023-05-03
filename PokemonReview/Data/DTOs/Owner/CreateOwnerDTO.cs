using System.ComponentModel.DataAnnotations;

namespace PokemonReview.Data.DTOs
{
    public class CreateOwnerDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Your First Name must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Your First Name must contain less than 100 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Your Last Name must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Your Last Name must contain less than 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Gym must contain at least 3 chars.")]
        [MaxLength(90, ErrorMessage = "Gym must contain less than 100 characters.")]
        public string Gym { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public int CountryID { get; set; }
    }
}

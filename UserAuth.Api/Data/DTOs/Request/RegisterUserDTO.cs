using System.ComponentModel.DataAnnotations;

namespace UserAuth.Api.Data.DTOs.Request
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(32, MinimumLength = 3)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "E-mail is required.")]
        [StringLength(90, MinimumLength = 3)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(90, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password confirm is required.")]
        [StringLength(90, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doens't match password.")]
        public string? PasswordConfirm { get; set; }
    }
}

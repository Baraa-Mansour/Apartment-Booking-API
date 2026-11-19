using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs
{
    public class LoginRequest
    {
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits.")]
        public string Phone { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(100)]
        public string Password { get; set; } = null!;
    }
}

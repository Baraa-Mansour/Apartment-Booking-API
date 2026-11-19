using Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Contracts.DTOs.auth
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits.")]
        public string Phone { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required, NotNull]
        [EnumDataType(typeof(UserRoles))]
        public UserRoles? Role { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required, NotNull]
        public DateOnly? BirthDate { get; set; }

        [Required]
        public string ProfilePhotoPath { get; set; } = null!;

        [Required]
        public string IdPhotoPath { get; set; } = null!;

        [EnumDataType(typeof(UserThemes))]
        public UserThemes? Theme { get; set; }

        [EnumDataType(typeof(UserLanguage))]
        public UserLanguage? Language { get; set; }
    }
}

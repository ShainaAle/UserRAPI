using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace UserRAPI.DTO
{
    public class UserforRegistrationDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        [AllowedTypes(new[] { "student", "admin", "teacher" }, ErrorMessage = "Type must be 'student', 'admin', or 'teacher'.")]
        public string? Type { get; set; }
    }
}

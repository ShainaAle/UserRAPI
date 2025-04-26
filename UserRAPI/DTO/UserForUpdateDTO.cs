using System.ComponentModel.DataAnnotations;

namespace UserRAPI.DTO
{
    public class UserForUpdateDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

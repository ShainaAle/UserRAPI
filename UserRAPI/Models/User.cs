using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserRAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

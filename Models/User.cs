using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ProfilePicture { get; set; } // Add this line for profile picture

        // Navigation property
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}

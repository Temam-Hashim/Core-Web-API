using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Navigation property
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}

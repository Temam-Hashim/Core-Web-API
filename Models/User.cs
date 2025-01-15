using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // navigation property for UserStock
        public ICollection<UserStock>? UserStocks { get; set; } // Navigation property to the join table

    }
}
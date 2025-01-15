using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Account
{
    public class LoginDTO
    {
        // public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
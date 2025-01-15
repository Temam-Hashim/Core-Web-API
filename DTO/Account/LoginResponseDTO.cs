using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.Account
{
    public class LoginResponseDTO
    {
        public string? Id { get; set;}
        // public string UserName { get;}
        public string? Email { get; set;}
        public string? Token { get; set; }
    }
}
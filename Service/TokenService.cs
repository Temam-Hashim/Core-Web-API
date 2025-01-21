using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                  new Claim(JwtRegisteredClaimNames.Email, user.Email),
                  new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                  new Claim(ClaimTypes.NameIdentifier, user.Id), // Use ClaimTypes.NameIdentifier
            };

            // Fetch user roles and add them as claims
            var roles = await _userManager.GetRolesAsync(user);
            // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            if (roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            else
            {
                // Log if no roles are found for the user
                Console.WriteLine("No roles found for the user.");
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // AddHours 
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

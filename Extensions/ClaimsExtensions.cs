using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class ClaimsExtensions
    {
        public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            // return user.Claims.SingleOrDefault(x=>x.Type.Equals("http://schemas.org.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;

            if (claimsPrincipal == null || claimsPrincipal.Identity == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal), "ClaimsPrincipal is null or not authenticated.");
            }

            return claimsPrincipal.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == JwtRegisteredClaimNames.Email)
                ?.Value;


        }
    }
}
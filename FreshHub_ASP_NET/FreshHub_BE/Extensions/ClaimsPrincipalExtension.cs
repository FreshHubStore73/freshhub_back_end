using FreshHub_BE.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreshHub_BE.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out int userId);

            return userId;
        }
    }
}

using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Services.TokenService
{
    public interface ITokenService
    {
        public string CreateToken(User user);
        
    }
}

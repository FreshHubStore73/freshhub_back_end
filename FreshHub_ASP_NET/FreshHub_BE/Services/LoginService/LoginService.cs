using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using FreshHub_BE.Services.TokenService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FreshHub_BE.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext dbContext;
        private readonly ITokenService tokenService;

        public LoginService(AppDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }
        public async Task<User> Exsist(UserLoginModel model)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
        }

        public UserModel Login(User user, UserLoginModel model)
        {
           // using var hmac = new HMACSHA512(user.Salt);
            //var password = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            
            //for (int i = 0; i < password.Length; i++)
            {
                //if (password[i] != user.Password[i])
                {
                    return default;
                }
                
            }
            return new UserModel
            {
                PhoneNumber = model.PhoneNumber,
                Token = tokenService.CreateToken(user),
            };
        }
    }
}

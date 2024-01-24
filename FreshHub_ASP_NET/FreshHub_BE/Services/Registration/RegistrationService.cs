

using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FreshHub_BE.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
       private readonly AppDbContext dbContext;
        public RegistrationService(AppDbContext appDbContext)
        {
            dbContext= appDbContext;
        }
        public async Task<bool> IsExists(UserRegistrationModel user)
        {
           return await dbContext.Users.AnyAsync(x => x.PhoneNumber == user.PhoneNumber);
        }

        public async Task<User> Registration(UserRegistrationModel user)
        {
            using var hmac = new HMACSHA512();
            var saveUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = " ", // spagetiCode
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                Salt = hmac.Key
            };

            dbContext.Users.Add(saveUser);
            await dbContext.SaveChangesAsync();

            return saveUser;
        }
    }
}

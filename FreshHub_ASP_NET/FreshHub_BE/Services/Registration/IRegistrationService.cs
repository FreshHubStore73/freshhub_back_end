using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;

namespace FreshHub_BE.Services.Registration
{
    public interface IRegistrationService
    {
        Task<bool> IsExists(UserRegistrationModel user);
        Task<User> Registration(UserRegistrationModel user);

    }
}

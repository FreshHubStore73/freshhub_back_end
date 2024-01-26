using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;

namespace FreshHub_BE.Services.LoginService
{
    public interface ILoginService
    {
        Task<User> Exsist(UserLoginModel model);
        UserModel Login(User user, UserLoginModel model);
    }
}

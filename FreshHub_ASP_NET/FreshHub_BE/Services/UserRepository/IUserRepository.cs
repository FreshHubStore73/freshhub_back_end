using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Services.UserRepository
{
    public interface IUserRepository
    {
        //public Task<User> Create(User user);
        //public Task Delete(int userId);
        public Task Update(User user);
        public Task<List<User>> GetAll();        
        public Task<User> GetById(int userId);
        public Task<bool> CheckPhoneNumber(string phoneNumber);
    }
}

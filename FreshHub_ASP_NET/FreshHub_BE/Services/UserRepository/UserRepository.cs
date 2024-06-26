﻿using FluentValidation;
using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Services.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IValidator<User> validator;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.validator = validator;
        }
        //public async Task<User> Create(User user)
        //{
        //    appDbContext.Users.Add(user);
        //    await appDbContext.SaveChangesAsync();
        //    return user;
        //}

        //public async Task Delete(int userId)
        //{
        //    appDbContext.Products.Remove(new Product() { Id = userId });
        //    await appDbContext.SaveChangesAsync();
        //}

        public async Task<List<User>> GetAll()
        {
            return await appDbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int userId)
        {
            return await appDbContext.Users
                .Include(x => x.Carts)
                .ThenInclude(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderDatails)
                .ThenInclude(x => x.Product)
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderStatus)                
                .FirstOrDefaultAsync(x => x.Id == userId);


        }

        public async Task Update(User user)
        {
            await validator.ValidateAndThrowAsync(user);
            appDbContext.Entry(user).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            
        }

        public async Task<Boolean> CheckPhoneNumber(string phoneNumber)
        {
            return await appDbContext.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}

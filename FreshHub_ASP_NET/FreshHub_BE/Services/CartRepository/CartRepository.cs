using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Services.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;

        public CartRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<CartItem> AddItem(int userId, CartItem item)
        {
           var user = await context.Users
                .Include(x=>x.Carts)
                .ThenInclude(x=>x.CartItems)
                .FirstAsync(x=>x.Id == userId);

            user.Carts.First().CartItems.Add(item);
            
            await context.SaveChangesAsync();

            return item;           
        }

        public async Task DeleteItem(CartItem item)
        {
            context.Entry(item).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            
        }

        public async Task<Cart> GetCart(int Id)
        {
            if (await context.Users.AnyAsync(x=>x.Id == Id))
            {
                return await context.Carts
                    .Include(x=>x.CartItems)
                    .Where(x => x.UserId == Id)
                    .SingleAsync();
            }
            throw new System.Exception("Invalid User..");
        }

        public async Task<CartItem> UpdateItem(CartItem item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return item;
        }
    }
}

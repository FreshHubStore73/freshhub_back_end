using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
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
        public async Task<CartItemResultModel> AddItem(int userId, CartItem item)
        {
            var user = await context.Users
                 .Include(x => x.Carts)
                 .ThenInclude(x => x.CartItems)
                 //.ThenInclude(x => x.Product)
                 .FirstAsync(x => x.Id == userId);

            var cart = user.Carts.FirstOrDefault();
            if (cart == default)
            {
                user.Carts.Add(new Cart());
            }

            user.Carts.First().CartItems.Add(item);

            await context.SaveChangesAsync();

            return new CartItemResultModel
            {
                Id = item.Id,
                Product = (await context.Products
                     .Include(x => x.Category)
                     .AsNoTracking()
                     .FirstOrDefaultAsync())!,
                Price = item.Price,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
        }

        public async Task<bool> DeleteItem(CartItem item, int userId)
        {

            if (!context.CartItems
                     .Include(x => x.Cart)
                     .ThenInclude(x => x.User)
                     .Any(x => x.Cart.User.Id == userId))
            {
                return false;
            }
            context.Entry(item).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Cart> GetCart(int Id)
        {
            if (await context.Users.AnyAsync(x => x.Id == Id))
            {
                return await context.Carts
                    .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Category)
                    .Where(x => x.UserId == Id)
                    .SingleAsync();
            }
            throw new System.Exception("Invalid User..");
        }

        public async Task<CartItem> UpdateItem(CartItem item, int userId)
        {
            if (!context.CartItems
                     .Include(x => x.Cart)
                     .ThenInclude(x => x.User)
                     .Any(x => x.Cart.User.Id == userId))
            {
                return null;
            }
            item.CartId = context.CartItems.AsNoTracking().First(x => x.Id == item.Id).CartId;
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return item;
        }
    }
}

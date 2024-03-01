using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;

namespace FreshHub_BE.Services.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(int Id);
        Task<CartItem> AddItem(int Id, CartItem item);
        Task<CartItem> UpdateItem(CartItem item, int userId);
        Task<bool> DeleteItem(CartItem item, int userId);

    }
}

using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Services.CartRepository
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(int Id);
        Task<CartItem> AddItem(int Id, CartItem item);
        Task<CartItem> UpdateItem(CartItem item);
        Task DeleteItem(CartItem item);

    }
}

using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class CartDto
    {
        public DateTime CreatedDate { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
    }
}

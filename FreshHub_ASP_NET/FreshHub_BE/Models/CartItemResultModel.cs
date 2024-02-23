using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class CartItemResultModel
    {
        public int Id { get; set; }        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }        
        public Product Product { get; set; }
    }
}

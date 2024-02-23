using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class CartItemModel
    {       
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }        
    }
}

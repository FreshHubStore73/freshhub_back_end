namespace FreshHub_BE.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }           
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; } = new();

    }
}

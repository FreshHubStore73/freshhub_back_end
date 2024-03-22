namespace FreshHub_BE.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public bool IsActive { get; set; } = true;

        public Category? Category { get; set; }

    }
}

namespace FreshHub_BE.Models
{
    public class ProductCreateModel
    {               
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
    }
}

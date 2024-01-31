using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class ProductResultModel
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }              
        public string CategoryName { get; set; }
    }
}

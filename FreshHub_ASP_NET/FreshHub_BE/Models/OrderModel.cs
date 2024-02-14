using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class OrderModel
    {       
        public TimeOnly OrderTimeOnly { get; set; }
        public DateOnly OrderDateOnly { get; set; }
        public string Recipient { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public int NumberPerson { get; set; }
        public bool Call { get; set; }
        public string Payment { get; set; }        
        public bool PaymentStatus { get; set; }
        public string StreetHouse { get; set; }
        public string Flat { get; set; }
        public string Floor { get; set; }
    }
}

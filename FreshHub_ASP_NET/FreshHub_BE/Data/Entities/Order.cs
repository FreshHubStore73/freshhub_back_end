namespace FreshHub_BE.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Recipient { get; set; }
        public string PhoneNumber { get; set; }
        //public TimeOnly? OrderTimeOnly { get; set; }
        //public DateOnly? OrderDateOnly { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string Comment { get; set; }
        public int NumberPerson { get; set; }
        public bool Call { get; set; }
        public string Payment { get; set; }
        public decimal CashSum { get; set; }
        public int DeliveryAddressId { get; set; }
        public int OrderStatusId { get; set; } = 1; 
        public bool PaymentStatus { get; set; }
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set;}
        public DeliveryAddress DeliveryAddress { get; set; }
        public List<OrderDatail> OrderDatails { get; set; }
    }
}

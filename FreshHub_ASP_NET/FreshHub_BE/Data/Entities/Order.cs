namespace FreshHub_BE.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int OrderStatusID { get; set; } // create table for order status ID
        public bool PaymentStatus { get; set; }
    }
}

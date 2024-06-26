﻿using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Models
{
    public class OrderWithOutCartModel
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string Recipient { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public int NumberPerson { get; set; }
        public bool Call { get; set; }
        public string Payment { get; set; }
        public decimal CashSum { get; set; }
        public bool PaymentStatus { get; set; }
        public string StreetHouse { get; set; }
        public string Flat { get; set; }
        public string Floor { get; set; }
        public int Id { get; set; }
        public List<CartItemModel> Items { get; set; }
    }

   
}

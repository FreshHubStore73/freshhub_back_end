﻿namespace FreshHub_BE.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}

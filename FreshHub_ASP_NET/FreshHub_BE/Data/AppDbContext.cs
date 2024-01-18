using FreshHub_BE.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base (options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDatail> OrderDatails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDatail>().HasKey(x => new { x.OrderId, x.ProductId }); //Fluent API
        }
    }
}

using FreshHub_BE.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Data
{
    public class AppDbContext: IdentityDbContext<User, 
                               Role, int, IdentityUserClaim<int>, 
                               UserRole, IdentityUserLogin<int>, 
                               IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions options): base (options)
        {

        }        
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDatail> OrderDatails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem>CartItems { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDatail>().HasKey(x => new { x.OrderId, x.ProductId });
           
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                Id = 1,
                Name = "In progress"
            });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                Id = 2,
                Name = "Done"
            });
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                Id = 3,
                Name = "Rejected"
            });

        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace FreshHub_BE.Data.Entities
{
    public class User:IdentityUser<int>
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string PhoneNumber { get; set; }        
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public ICollection<UserRole> UserRoles { get; set; }


    }
}

using Microsoft.AspNetCore.Identity;

namespace FreshHub_BE.Data.Entities
{
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }

    }
}

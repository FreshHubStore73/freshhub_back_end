using FreshHub_BE.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Data
{
    public class Seed
    {
        public static async Task SeedCategory(AppDbContext appDbContext)
        {
            if (await appDbContext.Categories.AnyAsync())
            {
                return;
            }

            appDbContext.Categories.AddRange(new Category { Name = "Burgers" }, new Category {Name = "Pizza" }, new Category { Name = "Salads" }, new Category { Name = "Desserts" });
            await appDbContext.SaveChangesAsync();
        }
        public static async Task SeedRole(RoleManager<Role> roleManager)
        {
            
            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }
            var roles = new List<Role> 
            {
                new Role { Name = "User"},
                new Role { Name = "Admin"},
                new Role { Name = "Moderator"}
            };
            foreach (var role in roles) 
            {
                await roleManager.CreateAsync(role);                   
            }            
        }

        public static async Task SeedUsers(UserManager<User> userManager)
        {
            var users = userManager.Users.ToList();
            if (await userManager.Users.AnyAsync())
            {
                return;
            }
            var admin = new User() 
            {
              FirstName = "Admin",
              LastName = "Admin",
              PhoneNumber = "123456789012",
              UserName = "Admin"
            };
            var resUser = await userManager.CreateAsync(admin, "Pa$$w0rd");
            var resRole = await userManager.AddToRolesAsync(admin, new[] { "Admin","Moderator",});
        }


    }
}

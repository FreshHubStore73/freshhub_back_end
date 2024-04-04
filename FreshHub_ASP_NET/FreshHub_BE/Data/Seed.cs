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

            appDbContext.Categories.AddRange(new Category { Name = "Burgers", Priority = 2 }, new Category { Name = "Pizza", Priority = 1 }, new Category { Name = "Salads", Priority = 3 }, new Category { Name = "Desserts", Priority = 4 });
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
            if (await userManager.Users.AnyAsync())
            {
                return;
            }
            var admin = new User()
            {
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "123456789012",
                UserName = "123456789012"
            };
            var resUser = await userManager.CreateAsync(admin, "Pa$$w0rd");
            var resRole = await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator", });

            var user = new User()
            {
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "111111111111",
                UserName = "111111111111"
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "User");
        }

        public static async Task SeedOrderStatus(AppDbContext appDb)
        {
            if (await appDb.OrderStatus.AnyAsync())
            {
                return;
            }

            await appDb.OrderStatus.AddRangeAsync(new OrderStatus { Name = "Done" }, new OrderStatus { Name = "In delivery" }, new OrderStatus { Name = "Acepted" });

            await appDb.SaveChangesAsync();
        }


    }
}

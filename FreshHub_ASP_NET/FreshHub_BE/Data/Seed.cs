using FreshHub_BE.Data.Entities;
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

    }
}

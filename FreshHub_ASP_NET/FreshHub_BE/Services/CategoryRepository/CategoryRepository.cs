using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Services.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task < List<Category>> GetAll()
        {
            return await appDbContext.Categories.ToListAsync();
        }
    }
}

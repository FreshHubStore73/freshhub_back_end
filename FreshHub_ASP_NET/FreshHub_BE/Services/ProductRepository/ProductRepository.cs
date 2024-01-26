using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Services.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Product> Create(Product product)
        {
            appDbContext.Products.Add(product);
            await appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task Delete(int productId)
        {
            appDbContext.Products.Remove(new Product() { Id = productId });
            await appDbContext.SaveChangesAsync();
        }

        public async Task <List<Product>> GetAll()
        {
            
            return await appDbContext.Products.Include(x=>x.Category).ToListAsync();
        }

        public async Task <List<Product>> GetAllByCategory(int categoryId)
        {
           return await appDbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task <Product?> GetById(int productId)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(x=>x.Id == productId);
        }

        public async Task Update(Product product)
        {
           appDbContext.Entry(product).State = EntityState.Modified;
           await appDbContext.SaveChangesAsync();
        }
    }
}

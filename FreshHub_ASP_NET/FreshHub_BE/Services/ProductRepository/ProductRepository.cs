﻿using FreshHub_BE.Data;
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
            return await appDbContext.Products
                .Where(p => p.Id == product.Id)
                .Include(p => p.Category)
                .FirstAsync();
        }

        public async Task Delete(int productId)
        {
            appDbContext.Products.Remove(new Product() { Id = productId });
            await appDbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {

            return appDbContext.Products.Include(x => x.Category);
        }

        public IQueryable<Product> GetAllByCategory(int categoryId)
        {
            return appDbContext.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryId);
        }

        public async Task<Product?> GetById(int productId)
        {
            return await appDbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task Update(Product product)
        {
            if (string.IsNullOrEmpty(product.PhotoUrl))
            {
                product.PhotoUrl = appDbContext.Products.First(x => x.Id == product.Id).PhotoUrl;
            }
            appDbContext.Entry(product).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();

        }

        public async Task<bool> IsProductIdExsist(int Id)
        {
            return await appDbContext.Products.AnyAsync(x => x.Id == Id);
        }

    }
}

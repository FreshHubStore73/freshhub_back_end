using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Services.ProductRepository
{
    public interface IProductRepository
    {
        public Task <Product> Create(Product product);
        public Task Update(Product product);
        public Task Delete(int productId);
        public IQueryable<Product> GetAll();
        public  IQueryable<Product> GetAllByCategory (int categoryId);
        public Task <Product> GetById(int productId);
        public Task<bool> IsProductIdExsist(int Id);
    }
}

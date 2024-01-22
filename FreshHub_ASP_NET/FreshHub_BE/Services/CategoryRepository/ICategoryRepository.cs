using FreshHub_BE.Data.Entities;

namespace FreshHub_BE.Services.CategoryRepository
{
    public interface ICategoryRepository
    {
       Task <List<Category>> GetAll();

    }
}

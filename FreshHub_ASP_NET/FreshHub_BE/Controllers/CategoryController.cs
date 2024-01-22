using FreshHub_BE.Data.Entities;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) 
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await categoryRepository.GetAll();
        }

    }
}

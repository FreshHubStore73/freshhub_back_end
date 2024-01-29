using FreshHub_BE.Data.Entities;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await productRepository.GetAll());
        }

        //[HttpPost("[action]")]
        //public async Task<ActionResult<Product>> Create([FromBody] Product product)
        //{
        //    return Ok(await productRepository.Create(product));
        //}

        [HttpGet("[action]/{Id}")]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllByCategory(int Id)
        {
            var categories = await categoryRepository.GetAll();
            if (!categories.Any(x => x.Id == Id))
            {
                return BadRequest("iNVALID CATEGORY ID");
            }
            return Ok(await productRepository.GetAllByCategory(Id));
        }

        [HttpGet("[action]/{Id}")]

        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            if (!await productRepository.IsProductIdExsist(Id))
            {
                return BadRequest("Product Id is not valid.");
            }
           return Ok(await productRepository.GetById(Id));

        }
    }
}

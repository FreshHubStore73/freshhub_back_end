using FluentValidation;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IValidator<ProductCreateModel> validator;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IValidator<ProductCreateModel> validator)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.validator = validator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductResultModel>>> GetAll()
        {
            return Ok((await productRepository.GetAll()).Select(p => new ProductResultModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Weight = p.Weight,
                Price = p.Price,
                Description = p.Description,
                PhotoUrl = p.PhotoUrl,
                CategoryName = p.Category.Name
            }));
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<ProductResultModel>> Create([FromBody] ProductCreateModel model)
        {
            await validator.ValidateAndThrowAsync(model);
            Product product = new Product();
            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.Weight = model.Weight;
            product.Price = model.Price;
            product.Description = model.Description;

            product.PhotoUrl = "kjsnkdnvaskdnv"; 

            var p = await productRepository.Create(product);
            return Ok(new ProductResultModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Weight = p.Weight,
                Price = p.Price,
                Description = p.Description,
                PhotoUrl = p.PhotoUrl,
                CategoryName = p.Category.Name
            });
        }


        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<IEnumerable<ProductResultModel>>> GetAllByCategory(int Id)
        {
            var categories = await categoryRepository.GetAll();
            if (!categories.Any(x => x.Id == Id))
            {
                return BadRequest("iNVALID CATEGORY ID");
            }
            return Ok((await productRepository.GetAllByCategory(Id)).Select(p => new ProductResultModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Weight = p.Weight,
                Price = p.Price,
                Description = p.Description,
                PhotoUrl = p.PhotoUrl,
                CategoryName = p.Category.Name
            }));
        }

        [HttpGet("[action]/{Id}")]

        public async Task<ActionResult<ProductResultModel>> GetProductById(int Id)
        {
            if (!await productRepository.IsProductIdExsist(Id))
            {
                return BadRequest("Product Id is not valid.");
            }
            var p = await productRepository.GetById(Id);

            return Ok(new ProductResultModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Weight = p.Weight,
                Price = p.Price,
                Description = p.Description,
                PhotoUrl = p.PhotoUrl,
                CategoryName = p.Category.Name
            });
        }
    }
}

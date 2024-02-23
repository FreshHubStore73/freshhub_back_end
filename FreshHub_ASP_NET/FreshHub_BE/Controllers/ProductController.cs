using FluentValidation;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string imagesPath;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IValidator<ProductCreateModel> validator;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IValidator<ProductCreateModel> validator, IWebHostEnvironment hostEnvironment)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.validator = validator;
            this.hostEnvironment = hostEnvironment;
            imagesPath ="Images";
        }

        private string CreatePath(string image)
        {
            return string.IsNullOrEmpty(image) ? null : Path.Combine(imagesPath, image);
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
                PhotoUrl =CreatePath(p.PhotoUrl),
                CategoryName = p.Category.Name
            }));
        }

        [Authorize(/*Policy = "ModeratorRole"*/)]
        [HttpPost("[action]")]

        public async Task<ActionResult<ProductResultModel>> Create([FromForm] ProductCreateModel model, IFormFile ?image)
        {
            await validator.ValidateAndThrowAsync(model);
            Product product = new Product();
            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.Weight = model.Weight;
            product.Price = model.Price;
            product.Description = model.Description;

            if (image != null)
            {
                product.PhotoUrl = image.FileName;
                var path = Path.Combine(hostEnvironment.WebRootPath, "Images", image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream); 
                }
            }
            else 
            {
                product.PhotoUrl = "";
            }

            var p = await productRepository.Create(product);
            return Ok(new ProductResultModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Weight = p.Weight,
                Price = p.Price,
                Description = p.Description,
                PhotoUrl = CreatePath(p.PhotoUrl),
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
                PhotoUrl = CreatePath(p.PhotoUrl),
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
                PhotoUrl = CreatePath(p.PhotoUrl),
                CategoryName = p.Category.Name
            });
        }
    }
}





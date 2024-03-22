using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Helpers;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace FreshHub_BE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IValidator<ProductCreateModel> validator;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IValidator<ProductCreateModel> validator, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.validator = validator;
            this.hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }



        [HttpGet("[action]")]
        public ActionResult<IEnumerable<ProductResultModel>> GetAll()
        {
            return Ok(productRepository.GetAll().ProjectTo<ProductResultModel>(mapper.ConfigurationProvider));
        }

        [Authorize(Policy = "ModeratorRole")]
        [HttpPost("[action]")]

        public async Task<ActionResult<ProductResultModel>> Create([FromForm] ProductCreateModel model, IFormFile? image)
        {
            await validator.ValidateAndThrowAsync(model);
            var product = mapper.Map<Product>(model);

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
            return Ok(mapper.Map<ProductResultModel>(p));
        }


        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<IEnumerable<ProductResultModel>>> GetAllByCategory(int Id)
        {
            var categories = await categoryRepository.GetAll();
            if (!categories.Any(x => x.Id == Id))
            {
                return BadRequest("iNVALID CATEGORY ID");
            }
            return Ok((productRepository.GetAllByCategory(Id)).ProjectTo<ProductResultModel>(mapper.ConfigurationProvider));
        }

        [HttpGet("[action]/{Id}")]

        public async Task<ActionResult<ProductResultModel>> GetProductById(int Id)
        {
            if (!await productRepository.IsProductIdExsist(Id))
            {
                return BadRequest("Product Id is not valid.");
            }
            var p = await productRepository.GetById(Id);

            return Ok(mapper.Map<ProductResultModel>(p));
        }

        [Authorize(Policy = "ModeratorRole")]
        [HttpPut("[action]/{Id}")]

        public async Task<ActionResult<ProductResultModel>> UpdateProduct(int Id, [FromForm] ProductCreateModel model, IFormFile? image)
        {
            await validator.ValidateAndThrowAsync(model);
            var product = mapper.Map<Product>(model);

            if (image != null)
            {
                product.PhotoUrl = image.FileName;
                var path = Path.Combine(hostEnvironment.WebRootPath, "Images", image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            product.Id = Id;
            await productRepository.Update(product);
            return Ok(product);
        }
    }
}





using AutoMapper;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Extensions;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CartRepository;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            int userId = User.GetUserId();
            var cart = await cartRepository.GetCart(userId);
            return Ok(mapper.Map<CartDto>(cart));
        }

        [HttpPost]

        public async Task<ActionResult> AddItem([FromBody] CartItemModel cartItemModel)
        {
            int userId = User.GetUserId();
            var cartItem = await cartRepository.AddItem(userId, mapper.Map<CartItem>(cartItemModel));
            return Ok(mapper.Map<CartItemResultModel>(cartItem));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int userId = User.GetUserId();
            var result = await cartRepository.DeleteItem(new CartItem {Id = id},userId);
           

            return (result == false) ? BadRequest() : Ok();
        }

        [HttpPut("{cartItemId}")]
        public async Task<ActionResult> Update(int cartItemId, [FromBody] CartItemModel cartItemModel)
        {
            int userId = User.GetUserId();
            var result = await cartRepository.UpdateItem(mapper.Map<CartItem>(cartItemModel), userId);

            var resultModel = mapper.Map<CartItem>(cartItemModel);

            return (result == null) ? BadRequest() : Ok(resultModel);
        }
    }
}

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

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            int userId = User.GetUserId();
            return Ok(await cartRepository.GetCart(userId));
        }

        [HttpPost]

        public async Task<ActionResult> AddItem([FromBody] CartItemModel cartItemModel)
        {
            int userId = User.GetUserId();

            var cartItem = await cartRepository.AddItem(userId, new Data.Entities.CartItem
            {
                ProductId = cartItemModel.ProductId,
                Quantity = cartItemModel.Quantity,
                Price = cartItemModel.Price
            });

            return Ok(cartItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int userId = User.GetUserId();
            var result = await cartRepository.DeleteItem(new Data.Entities.CartItem
            {
                Id = id
            }, userId);

            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{cartItemId}")]
        public async Task<ActionResult> Update(int cartItemId, [FromBody] CartItemModel cartItemModel)
        {
            int userId = User.GetUserId();
            var result = await cartRepository.UpdateItem(new Data.Entities.CartItem
            {
                Id = cartItemId,
                Price = cartItemModel.Price,
                Quantity = cartItemModel.Quantity,
                ProductId = cartItemModel.ProductId

            }, userId);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}

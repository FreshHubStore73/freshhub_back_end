using FluentValidation;
using FreshHub_BE.Enums;
using FreshHub_BE.Extensions;
using FreshHub_BE.Models;
using FreshHub_BE.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IValidator<OrderModel> validator;

        public OrderController(IOrderService orderService,
                               IValidator<OrderModel> validator)
        {
            this.orderService = orderService;
            this.validator = validator;
        }
        [HttpPost("[Action]")]

        public async Task<ActionResult<OrderResultModel>> Create([FromBody]OrderModel order)
        {
            await validator.ValidateAndThrowAsync(order);

            int userId = User.GetUserId();
            var result = await orderService.Create(order, userId);
            return Ok(result);
        }

        [HttpGet("[Action]")]

        public async Task<ActionResult> GetAll()
        {
            int userId = User.GetUserId();

            var result = await orderService.GetAll();
            return Ok(result);
        }

        [HttpGet("[Action]")]

        public async Task<ActionResult> GetAllByUser()
        {
            int userId = User.GetUserId();

            var result = await orderService.GetAllByUser(userId);
            return Ok(result);
        }

        [HttpGet("[Action]/{id}")]

        public async Task<ActionResult> GetOrderInfo(int id)
        {
            var result = await orderService.GetOrderInfo(id);
            return Ok(result);
        }

        [HttpPut("[action]/{id}")]

        public async Task<ActionResult> Rejected(int id)
        {
            await orderService.Rejected(id);
            return Ok();
        }

        [HttpPut("[Action]/{id}")]

        public async Task<ActionResult> Status(int id, [FromQuery] PaymentEnum status)
        {
            await orderService.Status(id, status);
            return Ok();
        }

        [HttpPut("[Action]")]

        public async Task<ActionResult> Update([FromBody] OrderModel orderModel)
        {
            int userId = User.GetUserId();

            await orderService.Update(orderModel, userId);
            return Ok();
        }
    }
}

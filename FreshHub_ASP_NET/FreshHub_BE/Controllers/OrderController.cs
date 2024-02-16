using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost("[Action]")]


        public async Task<ActionResult<OrderResultModel>> Create (OrderModel order)
        {
            return Ok();
        }
    }
}

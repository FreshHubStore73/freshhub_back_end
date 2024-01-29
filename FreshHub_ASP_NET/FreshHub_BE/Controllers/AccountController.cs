using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index([FromHeader] string Authorization)
        {
            return Ok("This is AccountController.");
        }
    }
}

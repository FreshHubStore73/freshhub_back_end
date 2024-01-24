using FreshHub_BE.Models;
using FreshHub_BE.Services.Registration;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegistrationService registrationService;

        public UserController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel user)
        {
            if (await registrationService.IsExists(user))
            {
                return BadRequest("Phone is taken.");
            }

            await registrationService.Registration(user);

            return Ok();
        }


    }
}

using FreshHub_BE.Models;
using FreshHub_BE.Services.LoginService;
using FreshHub_BE.Services.Registration;
using Microsoft.AspNetCore.Mvc;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        private readonly ILoginService loginService;

        public UserController(IRegistrationService registrationService, ILoginService loginService)
        {
            this.registrationService = registrationService;
            this.loginService = loginService;
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

        [HttpPost("[action]")]

        public async Task<ActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await loginService.Exsist(model);

            if (user == null)
            {
                return Unauthorized("Invalid phone or password.");
            }

            var result = loginService.Login(user, model);

            if (result is null)
            {
                return Unauthorized("Invalid phone or password.");
            }
            return Ok(result);
        }


    }
}

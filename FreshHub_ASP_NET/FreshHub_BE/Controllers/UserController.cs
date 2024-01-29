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
            user.Password = user.Password.Trim(' ');
            if (user.Password.Length < 4 || user.Password.Length > 8) 
            {
                return Unauthorized("Bad password.");
            }

            user.PhoneNumber = user.PhoneNumber.Trim(' ');
            if (user.PhoneNumber.Length != 12)
            {
                return Unauthorized("Bad nomber phone.");
            }
            if (await registrationService.IsExists(user))
            {
                return BadRequest("Phone is taken.");
            }

            user.FirstName = user.FirstName.Trim(' ');
            if (user.FirstName.Length < 3 || user.FirstName.Length > 15)
            {
                return Unauthorized("Bad Firstname.");
            }
            if (await registrationService.IsExists(user))
            {
                return BadRequest("Firstname is taken.");
            }

            user.LastName = user.LastName.Trim(' ');
            if (user.LastName.Length < 3 || user.LastName.Length > 15)
            {
                return Unauthorized("Bad Lastname.");
            }
            if (await registrationService.IsExists(user))
            {
                return BadRequest("Lastname is taken.");
            }


            await registrationService.Registration(user);

            return Ok();
        }

        [HttpPost("[action]")]

        public async Task<ActionResult> Login([FromBody] UserLoginModel model)
        {


            model.Password = model.Password.Trim(' ');
            if (model.Password.Length < 4 || model.Password.Length > 8)
            {
                return Unauthorized("Bad password.");
            }

            model.PhoneNumber = model.PhoneNumber.Trim(' ');
            if (model.PhoneNumber.Length != 12)
            {
                return Unauthorized("Bad nomber phone.");
            }

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

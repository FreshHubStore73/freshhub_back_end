using FluentValidation;
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
        private readonly IValidator<UserLoginModel> loginValidator;
        private readonly IValidator<UserRegistrationModel> registrationValidator;

        public UserController(IRegistrationService registrationService,
                              ILoginService loginService, 
                              IValidator<UserLoginModel> loginValidator, IValidator<UserRegistrationModel> registrationValidator)
        {
            this.registrationService = registrationService;
            this.loginService = loginService;
            this.loginValidator = loginValidator;
            this.registrationValidator = registrationValidator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel user)
        {
            await registrationValidator.ValidateAndThrowAsync(user);                      

            await registrationService.Registration(user);

            return Ok();
        }

        [HttpPost("[action]")]

        public async Task<ActionResult> Login([FromBody] UserLoginModel model)
        {          

            await loginValidator.ValidateAndThrowAsync(model);

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

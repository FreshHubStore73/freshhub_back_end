﻿using AutoMapper;
using FluentValidation;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using FreshHub_BE.Services.TokenService;
using FreshHub_BE.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FreshHub_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {        
        private readonly IValidator<UserLoginModel> loginValidator;
        private readonly IValidator<UserRegistrationModel> registrationValidator;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserController(IValidator<UserLoginModel> loginValidator,            IValidator<UserRegistrationModel> registrationValidator,
                              UserManager<User> userManager, ITokenService tokenService,
                              IUserRepository userRepository,
                              RoleManager<Role> roleManager,
                              IMapper mapper)
        {            
            this.loginValidator = loginValidator;
            this.registrationValidator = registrationValidator;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel user)
        {
            await registrationValidator.ValidateAndThrowAsync(user);
            var dataBaseUser = mapper.Map<User>(user);

            var roles = roleManager.Roles.ToList();
            var result = await userManager.CreateAsync(dataBaseUser, user.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var roleResult = await userManager.AddToRoleAsync(dataBaseUser, roles[0].Name);
            if (!roleResult.Succeeded)
            {
                return BadRequest(result.Errors);
            }           

            return Ok();
        }

        [HttpPost("[action]")]

        public async Task<ActionResult> Login([FromBody] UserLoginModel model)
        {

            await loginValidator.ValidateAndThrowAsync(model);

            var user = await userManager.Users
                                  .SingleOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null)
            {
                return Unauthorized("Invalid phone or password.");
            }
            var result = await userManager.CheckPasswordAsync(user, model.Password);
            if (result != true)
            {
                return Unauthorized("Invalid phone or password.");
            }

            return Ok(new UserModel
            {
                PhoneNumber = model.PhoneNumber,
                Token = tokenService.CreateToken(user),
            });

        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<User>> GetInfoAboutUser()
        {
            var id_ = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            int id = int.Parse(id_!.Value);

            return await userRepository.GetById(id);
        }


    }
}

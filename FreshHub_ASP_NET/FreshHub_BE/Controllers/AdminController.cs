﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshHub_BE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly IMapper mapper;

        public AdminController(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }


        [Authorize(Policy = "ModeratorRole")]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetUsersWithRole()
        {
            var users = await userManager.Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .OrderBy(u => u.Id)
                .ProjectTo<UserWithRoleModels>(mapper.ConfigurationProvider)
                //.Select(u => mapper.Map<UserWithRoleModels>(u))
                .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "ModeratorRole")]
        [HttpPost("[action]/{userId}")]

        public async Task<ActionResult> ChangeRole(int userId, [FromQuery] string roles)
        {
            if (string.IsNullOrEmpty(roles))
            {
                return BadRequest("Roles is must exiests.");
            }

            var selectedRoles = roles.Split(',');
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                return BadRequest("Field to addet roles.");
            }

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                return BadRequest("Field to remove from roles");
            }

            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpGet("[action]")]

        public async Task< string> Test()
        {
            var user = HttpContext.User;
            var user1 = await  userManager.GetUserAsync(HttpContext.User);
            var role =await userManager.GetRolesAsync(user1);
            //var role = 
            return " ";
        }

    }
}

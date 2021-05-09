using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Constants;
using SchoolRegistration.Controllers.Resources;
using SchoolRegistration.Models;
using SchoolRegistration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }


        [HttpGet("users")]
        [Authorize(Roles = "HR,Administrator,Staff")]
        // [Authorize(Roles = "HR,Administrator")]
        public async Task<List<UserResource>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var result = mapper.Map<List<ApplicationUser>, List<UserResource>>(users);
            return result;
        }

        [HttpGet("user")]
        [Authorize()]
        // [Authorize(Roles = "HR,Administrator")]
        public async Task<UserResource> GetUser(string id)
        {
            var user = await _userService.GetUserAsync(id);
            var result = mapper.Map<ApplicationUser, UserResource>(user);

            return result;
        }

        [HttpPost("AcceptUser")]
        [Authorize(Roles = "HR,Administrator")]
        public async Task<IActionResult> AcceptUser(string id)
        {

            var user = await _userService.AcceptUser(id);
            if (user == null)
            {
                return NotFound("User not available");
            }
            var result = mapper.Map<ApplicationUser, UserResource>(user);
            return Ok(result);
        }

        [HttpPost("RejectUser")]
        [Authorize(Roles = "HR,Administrator")]
        public async Task<IActionResult> RejectUser(string id)
        {

            var user = await _userService.RejectUser(id);
            if (user == null)
            {
                return NotFound("User not available");
            }
            var result = mapper.Map<ApplicationUser, UserResource>(user);
            return Ok(result);
        }


    }
}

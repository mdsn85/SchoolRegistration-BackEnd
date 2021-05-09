using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }
    }
}

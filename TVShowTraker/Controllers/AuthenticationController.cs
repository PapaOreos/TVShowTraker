using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TVShowTraker.Models.Auth;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthenticationController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] Login login)
        {
            var response = _service.Authenticate(login);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register([FromBody] Register register)
        {
            var response = _service.Register(register);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}

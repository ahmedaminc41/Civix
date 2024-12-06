using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Service.Contracts;
using Civix.App.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Civix.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto is null) return BadRequest("Invalid Registeration !!"); // 400
             
            var result = await  _authService.RegisterAsync(registerDto);

            if (result is null) return BadRequest("Invalid Registeration !!");

            return Ok(result);

        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto is null) return Unauthorized("Invalid Login !!"); // 400

            var result = await _authService.LoginAsync(loginDto);

            if (result is null) return Unauthorized("Invalid Login !!");

            return Ok(result);

        }

    }
}

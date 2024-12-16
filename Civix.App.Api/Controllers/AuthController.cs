using Civix.App.Api.Extensions;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Dtos.Otp;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Civix.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CivixDbContext _context;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager, CivixDbContext context)
        {
            _authService = authService;
            _userManager = userManager;
            _context = context;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            var result = await _authService.RegisterAsync(registerDto);
            return result.IsSuccess ?
                Ok(result.Value) :
                result.ToProblem(StatusCodes.Status400BadRequest, "Bad Request");

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            return result.IsSuccess ?
                Ok(result.Value) :
                result.ToProblem(StatusCodes.Status401Unauthorized, "Unauthorized u r not");

        }


        [HttpPost("password-reset")]
        public async Task<IActionResult> RequestResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.RequestResetPasswordAsync(resetPasswordDto);

            return result.IsSuccess ?
                Ok(result.Value) :
                result.ToProblem(StatusCodes.Status400BadRequest, "Bad Request");

        }


        [HttpPost("check-otp")]
        public async Task<IActionResult> CheckOtp(CheckOtpDto checkOtpDto)
        {

            var result = await _authService.CheckOtpAsync(checkOtpDto);
            return result.IsSuccess ?
               Ok(result.Value) :
               result.ToProblem(StatusCodes.Status400BadRequest, "Bad Request");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _authService.ResetPasswordAsync(changePasswordDto);
            return result.IsSuccess ?
               Ok(result.Value) :
               result.ToProblem(StatusCodes.Status400BadRequest, "Bad Request");

        }


    }
}

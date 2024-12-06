using Civix.App.Api.Helpers;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Dtos.Otp;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Repositories.Data;
using Civix.App.Services.Auth;
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


        [HttpPost("password-reset")]
        public async Task<IActionResult> RequestResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if(resetPasswordDto is null) return BadRequest("Invalid Email");

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null) return BadRequest("Invalid Email");

            // Generate Otp 

            var otp = OtpSettings.GenerateOtp();
            var otpRecord = new OtpRecord()
            {
                Email = user.Email,
                Otp = OtpSettings.HashOtp(otp),
                ExpiryTime = DateTime.UtcNow.AddMinutes(10),
            };

            _context.OtpRecords.Add(otpRecord);
            var count = await _context.SaveChangesAsync();
            if (count <= 0) return BadRequest("Invalid Reset Password, Please Try Again !");

            // Send Email

            var email = new Email()
            {
                To = user.Email,
                Body = $"Your OTP For Password Reset is {otp}, It Will Be Expire in 1 Minutes. :)",
                Subject = "Password Reset Otp"
            };

            EmailSettings.SendEmail(email);

            return Ok("OTP Sent To Your Email, Please Check Your Email");

        }


        [HttpPost("check-otp")]
        public async Task<IActionResult> CheckOtp(CheckOtpDto checkOtpDto)
        {
            if(checkOtpDto is null) return BadRequest("Invalid Otp");

            var otpRecord = _context.OtpRecords.Where( X => X.Email == checkOtpDto.Email && X.ExpiryTime > DateTime.UtcNow).FirstOrDefault();

            if (otpRecord is null) return BadRequest("Invalid Or expired OTP !");
            if (!OtpSettings.VerifyOtp(otpRecord.Otp, checkOtpDto.InputOtp)) return BadRequest("Invalid OTP !");

            var user = await _userManager.FindByEmailAsync(checkOtpDto.Email);
            if (user is null) return BadRequest("Invalid Reset Password !");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(new { Message = "OTP is valid !", Token = token });


        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto is null) return BadRequest("Invalid Reset Password");

            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user is null) return BadRequest("Invalid Reset Password !");

            var result = await _userManager.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.NewPassword);
            if (!result.Succeeded) BadRequest("Invalid Reset Password !");

            return Ok(new { Message = "Password Reset Successfully" });

        }


    }
}

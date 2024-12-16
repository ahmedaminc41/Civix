using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Abstractions;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Dtos.Otp;
using Civix.App.Core.Entities;
using Civix.App.Core.Errors;
using Civix.App.Core.Helpers;
using Civix.App.Core.Service.Contracts.Auth;
using Civix.App.Core.Service.Contracts.Token;
using Civix.App.Repositories.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Civix.App.Services.Auth
{
    public class AuthServic : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly CivixDbContext _context;
        private readonly ITokenService _tokenService;

        // UserManager
        // SignInManager
        // RoleManager

        public AuthServic(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            CivixDbContext context,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Result<RegisterReturnDto>> LoginAsync(LoginDto loginDto)
        {
            if (loginDto is null) return Result.Failure< RegisterReturnDto>(UserErrors.InvalidCreadentials);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return Result.Failure<RegisterReturnDto>(UserErrors.InvalidCreadentials);
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Result.Failure<RegisterReturnDto>(UserErrors.InvalidCreadentials);

            var response = new RegisterReturnDto()
            {
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Result.Success(response);

        }
        public async Task<Result<RegisterReturnDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto is null) return Result.Failure<RegisterReturnDto>(UserErrors.InvalidRegistration);

            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user is not null) return Result.Failure<RegisterReturnDto>(UserErrors.ExistsEmail); ;

            user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return Result.Failure<RegisterReturnDto>(UserErrors.InvalidRegistration); ;

            var response = new RegisterReturnDto()
            {
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Result.Success(response);

        }
        public async Task<Result<string>> RequestResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto is null) return Result.Failure<string>(UserErrors.InvalidResetPassword);

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null) return Result.Failure<string>(UserErrors.InvalidEmail);

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
            if (count <= 0) return Result.Failure<string>(UserErrors.InvalidResetPassword);

            // Send Email

            var email = new Email()
            {
                To = user.Email,
                Body = $"Your OTP For Password Reset is {otp}, It Will Be Expire in 1 Minutes. :)",
                Subject = "Password Reset Otp"
            };
            EmailSettings.SendEmail(email);
            return Result.Success("OTP Sent To Your Email, Please Check Your Email");

        }
        public async Task<Result<object>> CheckOtpAsync(CheckOtpDto checkOtpDto)
        {
            if (checkOtpDto is null) return Result.Failure<object>(UserErrors.InvalidOtp);

            var otpRecord = _context.OtpRecords.Where(X => X.Email == checkOtpDto.Email && X.ExpiryTime > DateTime.UtcNow).OrderByDescending(X => X.ExpiryTime).FirstOrDefault();

            if (otpRecord is null) return Result.Failure<object>(UserErrors.InvalidOtp);
            if (!OtpSettings.VerifyOtp(otpRecord.Otp, checkOtpDto.InputOtp)) return Result.Failure<object>(UserErrors.InvalidOtp);

            var user = await _userManager.FindByEmailAsync(checkOtpDto.Email);
            if (user is null) return Result.Failure<object>(UserErrors.InvalidEmail);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Result.Success(new { Message = "OTP is valid !", Token = token } as object);
        }
        public async Task<Result<object>> ResetPasswordAsync(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto is null) return Result.Failure<object>(UserErrors.InvalidResetPassword);

            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user is null) return Result.Failure<object>(UserErrors.InvalidEmail);
            

            // TODO: Select Using Hashed OTP Ya 7aywan !! :(
            var otpRecord = _context.OtpRecords.Where(X => X.Email == changePasswordDto.Email && X.ExpiryTime > DateTime.UtcNow).OrderByDescending(X => X.ExpiryTime).FirstOrDefault();

            if (otpRecord is null) return Result.Failure<object>(UserErrors.InvalidResetPassword);

            var result = await _userManager.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.NewPassword);
            
            if (!result.Succeeded) return Result.Failure<object>(UserErrors.InvalidResetPassword);

            _context.OtpRecords.Remove(otpRecord);
            await _context.SaveChangesAsync();

            return Result.Success(new { Message = "Password Reset Successfully" } as object);

        }


    }
}

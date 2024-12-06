using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Auth;
using Microsoft.AspNetCore.Identity;

namespace Civix.App.Services.Auth
{
    public class AuthServic : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        // UserManager
        // SignInManager
        // RoleManager

        public AuthServic(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<RegisterReturnDto> LoginAsync(LoginDto loginDto)
        {
            if (loginDto is null) return null;

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;

            return new RegisterReturnDto()
            {
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                Token = "TODO"
            };

        }

        public async Task<RegisterReturnDto> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto is null) return null;

            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user is not null) return null;

            user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return null;

            return new RegisterReturnDto()
            {
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                Token = "TODO"
            };

        }
    }
}

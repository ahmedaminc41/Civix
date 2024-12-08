using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Abstractions;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Dtos.Otp;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Service.Contracts.Auth
{
    public interface IAuthService
    {
        Task<Result<RegisterReturnDto>> LoginAsync(LoginDto loginDto);
        Task<Result<RegisterReturnDto>> RegisterAsync(RegisterDto registerDto);
        public Task<Result<string>> RequestResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        public Task<Result<object>> CheckOtpAsync(CheckOtpDto checkOtpDto);
        public Task<Result<object>> ResetPasswordAsync(ChangePasswordDto changePasswordDto);

    }
}

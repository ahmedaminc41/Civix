using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Abstractions;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Service.Contracts.Auth
{
    public interface IAuthService
    {
        // Login
        Task<Result<RegisterReturnDto>> LoginAsync(LoginDto loginDto);
        // Register
        Task<Result<RegisterReturnDto>> RegisterAsync(RegisterDto registerDto);

        // resetpassword

    }
}

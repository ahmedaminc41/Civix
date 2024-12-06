using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Dtos.Auth;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Service.Contracts
{
    public interface IAuthService
    {
        // Login
        Task<RegisterReturnDto> LoginAsync(LoginDto loginDto);
        // Register
        Task<RegisterReturnDto> RegisterAsync(RegisterDto registerDto);

        // resetpassword

    }
}

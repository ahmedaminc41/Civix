using Civix.App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Service.Contracts.Token
{
    public interface IToken
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> _userManager);

    }
}

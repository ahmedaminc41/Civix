using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Abstractions;

namespace Civix.App.Core.Errors
{
    public static class UserErrors
    {
        public static readonly Error InvalidCreadentials = new("User.InvalidCreadentials", "Invalid Email or Password");
        public static readonly Error InvalidRegistration = new("User.InvalidRegistration", "Invalid Registration");
        public static readonly Error ExistsEmail = new("User.ExistsEmail", "This email is already exists");
    }
}

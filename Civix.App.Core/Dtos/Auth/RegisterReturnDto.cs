using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Dtos.Auth
{
    public class RegisterReturnDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

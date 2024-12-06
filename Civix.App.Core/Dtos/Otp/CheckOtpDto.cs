using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Dtos.Otp
{
    public class CheckOtpDto
    {
        public string Email { get; set; }
        public string InputOtp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Dtos.Otp
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match the confirmed password :(")]
        public string ConfirmedPassword { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Dtos.Auth
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The confirmed password does not match the password !")]
        public string ConfirmedPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}

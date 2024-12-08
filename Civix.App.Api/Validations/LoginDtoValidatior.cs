using Civix.App.Core.Dtos.Auth;
using FluentValidation;

namespace Civix.App.Api.Validations
{
    public class LoginDtoValidatior : AbstractValidator<LoginDto>
    {
        public LoginDtoValidatior()
        {
            RuleFor(X => X.Email)
              .NotEmpty()
              .EmailAddress();

            RuleFor(X => X.Password)
             .NotEmpty();
        }
    }
}

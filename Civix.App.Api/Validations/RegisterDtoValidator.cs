using Civix.App.Core.Abstractions.Consts;
using Civix.App.Core.Dtos.Auth;
using FluentValidation;

namespace Civix.App.Api.Validations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(X => X.FirstName)
             .NotEmpty();


            RuleFor(X => X.LastName)
             .NotEmpty();

            RuleFor(X => X.Email)
                .NotEmpty()
                .EmailAddress();


            RuleFor(X => X.Password)
             .NotEmpty()
             .Matches(RegexPatterns.Password)
             .WithMessage("Password should be at least 8 digits and should contain lowercase, nonAlphanumeric and uppercase letters.");

            RuleFor(X => X.ConfirmedPassword)
           .NotEmpty()
           .Equal(X => X.Password)
           .WithMessage("Confirmed password does not match the password.");
        }
    }
}

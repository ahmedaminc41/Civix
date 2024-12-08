using Civix.App.Core.Abstractions.Consts;
using Civix.App.Core.Dtos.Otp;
using FluentValidation;

namespace Civix.App.Api.Validations
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(X => X.Email)
             .NotEmpty()
             .EmailAddress();

            RuleFor(X => X.NewPassword)
             .NotEmpty()
             .Matches(RegexPatterns.Password)
             .WithMessage("Password should be at least 8 digits and should contain lowercase, nonAlphanumeric and uppercase letters.");

            RuleFor(X => X.ConfirmedPassword)
           .NotEmpty()
           .Equal(X => X.NewPassword)
           .WithMessage("Confirmed password does not match the password.");
        }
    }
}

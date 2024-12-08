using Civix.App.Core.Dtos.Otp;
using FluentValidation;

namespace Civix.App.Api.Validations
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(X => X.Email)
                    .NotEmpty()
                    .EmailAddress();
        }
    }
}

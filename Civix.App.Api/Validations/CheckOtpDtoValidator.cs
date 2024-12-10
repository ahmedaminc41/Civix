using Civix.App.Core.Dtos.Otp;
using FluentValidation;

namespace Civix.App.Api.Validations
{
    public class CheckOtpDtoValidator : AbstractValidator<CheckOtpDto>
    {
        public CheckOtpDtoValidator()
        {
            RuleFor(X => X.Email)
                    .NotEmpty()
                    .EmailAddress();

            RuleFor(X => X.InputOtp)
                    .NotEmpty()
                    .Length(6);

        }
    }
}

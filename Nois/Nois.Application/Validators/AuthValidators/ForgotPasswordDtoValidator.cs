using FluentValidation;
using Nois.Application.DTOs.AuthDtos;

namespace Nois.Application.Validators.AuthValidators
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}

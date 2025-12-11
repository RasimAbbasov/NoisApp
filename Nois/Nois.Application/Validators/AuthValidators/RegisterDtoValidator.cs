using FluentValidation;
using Nois.Application.DTOs.AuthDtos;

namespace Nois.Application.Validators.AuthValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}

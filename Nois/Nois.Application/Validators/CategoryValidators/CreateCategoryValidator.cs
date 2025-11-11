using FluentValidation;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Validators.CategoryValidators
{
    public class CreateCategoryValidator: AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
        }
    }
}

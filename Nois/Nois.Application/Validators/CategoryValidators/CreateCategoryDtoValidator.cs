using FluentValidation;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Validators.CategoryValidators
{
    public class CreateCategoryDtoValidator: AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
        }
    }
}

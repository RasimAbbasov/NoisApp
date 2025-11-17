using FluentValidation;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Validators.CategoryValidators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator() 
        {
          RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}

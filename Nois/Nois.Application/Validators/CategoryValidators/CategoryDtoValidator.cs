using FluentValidation;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Validators.CategoryValidators
{
    public class CategoryDtoValidator : AbstractValidator<CategorySummaryDto>
    {
        public CategoryDtoValidator() 
        {
          RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}

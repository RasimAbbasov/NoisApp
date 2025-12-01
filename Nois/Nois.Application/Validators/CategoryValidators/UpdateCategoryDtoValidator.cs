using FluentValidation;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;

namespace Nois.Application.Validators.CategoryValidators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator() 
        {
          RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}

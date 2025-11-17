using FluentValidation;
using Nois.Application.DTOs.ColorDtos;

namespace Nois.Application.Validators.ColorValidators
{
    public class ColorDtoValidator :AbstractValidator<ColorDto>
    {
        public ColorDtoValidator() 
        {
         RuleFor(x=> x.Name).NotEmpty().WithMessage("Name is Required");
         RuleFor(x => x.Code).NotEmpty().WithMessage("Code is Required");
         RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is Required");
        }
    }
}

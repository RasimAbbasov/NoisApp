

using FluentValidation;
using Nois.Application.DTOs.SizeDtos;

namespace Nois.Application.Validators.SizeValidators
{
    public class UpdateSizeDtoValidator : AbstractValidator<UpdateSizeDto>
    {
        public UpdateSizeDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is required");
        }
    }
}

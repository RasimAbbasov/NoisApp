using FluentValidation;
using Nois.Application.DTOs.ProductVariantDtos;

namespace Nois.Application.Validators.ProductVariantValidators
{
    public class UpdateProductVariantDtoValidator : AbstractValidator<UpdateProductVariantDto>
    {
        public UpdateProductVariantDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.SizeId).NotEmpty();
            RuleFor(x=> x.ProductId).NotEmpty();
        }
    }
}

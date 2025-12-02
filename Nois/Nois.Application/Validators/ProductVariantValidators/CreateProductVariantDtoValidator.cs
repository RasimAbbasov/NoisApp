using FluentValidation;
using Nois.Application.DTOs.ProductVariantDtos;
namespace Nois.Application.Validators.ProductVariantValidators
{
    public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
    {
        public CreateProductVariantDtoValidator()
        {
            RuleFor(x => x.ColorId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.SizeId).NotEmpty();
        }
    }
}

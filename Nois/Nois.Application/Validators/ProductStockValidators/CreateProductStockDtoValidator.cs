using FluentValidation;
using Nois.Application.DTOs.ProductStockDtos;

namespace Nois.Application.Validators.ProductStockValidators
{
    public class CreateProductStockDtoValidator : AbstractValidator<CreateProductStockDto>
    {
        public CreateProductStockDtoValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required.");
            RuleFor(x => x.ProductVariantId).NotEmpty().WithMessage("ProductVariantId is required.");
        }
    }
}

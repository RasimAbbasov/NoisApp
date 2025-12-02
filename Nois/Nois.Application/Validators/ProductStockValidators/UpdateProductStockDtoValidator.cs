using FluentValidation;
using Nois.Application.DTOs.ProductStockDtos;

namespace Nois.Application.Validators.ProductStockValidators
{
    public class UpdateProductStockDtoValidator : AbstractValidator<UpdateProductStockDto>
    {
        public UpdateProductStockDtoValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required.");
            RuleFor(x => x.ProductVariantId).NotEmpty().WithMessage("ProductVariantId is required.");
        }
    }
}

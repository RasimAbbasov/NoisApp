using FluentValidation;
using Nois.Application.DTOs.ProductDtos;

namespace Nois.Application.Validators.ProductValidators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price cannot be less than 0.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId cannot be less than 0.");
        }
    }
}

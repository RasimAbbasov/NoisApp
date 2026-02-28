using FluentValidation;
using Nois.Application.DTOs.WishlistDtos;

namespace Nois.Application.Validators.WishlistValidators
{
    public class CreateWishlistItemDtoValidator : AbstractValidator<CreateWishlistItemDto>
    {
		public CreateWishlistItemDtoValidator()
		{
			RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is Required");
			RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is Required");
		}
	}
}

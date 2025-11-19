using FluentValidation;
using Nois.Application.DTOs.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.Validators.SizeValidators
{
    public class CreateSizeDtoValidator : AbstractValidator<CreateSizeDto>
    {
        public CreateSizeDtoValidator()
        {
            RuleFor(x=> x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is required");
        }
    }
}

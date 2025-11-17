using FluentValidation;
using Nois.Application.DTOs.ColorDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.Validators.ColorValidators
{
    public class CreateColorDtoValidator : AbstractValidator<CreateColorDto>
    {
        public CreateColorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is Required");
            RuleFor(x => x.SortOrder).NotEmpty().WithMessage("SortOrder is Required");
        }
    }
}

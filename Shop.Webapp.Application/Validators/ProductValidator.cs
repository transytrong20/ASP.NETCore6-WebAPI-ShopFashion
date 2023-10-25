using FluentValidation;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ConstsDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Webapp.Application.Validators
{
    public class CreateOrUpdateProductValidator : AbstractValidator<CreateOrUpdateProductModel>
    {
        public CreateOrUpdateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageError.NotEmpty);
            RuleFor(x => x.Description).NotEmpty().WithMessage(MessageError.NotEmpty);
            RuleFor(x => x.Price).NotEmpty().WithMessage(MessageError.NotEmpty).Must(x => x > 0).WithMessage("Price must be greater than 0");
        }
    }
}

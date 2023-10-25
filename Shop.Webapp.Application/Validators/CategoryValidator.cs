using FluentValidation;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.Webapp.Application.Validators
{
    public class CreateOrUpdateCategoryValidator : AbstractValidator<CreateOrUpdateCategoryModel>
    {
        public CreateOrUpdateCategoryValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().WithMessage(MessageError.NotEmpty);
                //.Must(_ => _.ValidateUsername()).WithMessage(MessageError.UnValid);
            RuleFor(_ => _.Index)
                .Must(_ => _ > 0).WithMessage(MessageError.UnValid);
        }
    }
}

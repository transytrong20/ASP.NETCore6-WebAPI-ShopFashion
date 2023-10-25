using FluentValidation;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.Webapp.Application.Validators
{
    public class AddRoleValidators : AbstractValidator<AddRoleModel>
    {
        public AddRoleValidators()
        {
            RuleFor(_ => _.Name)
                .NotEmpty().WithMessage(MessageError.NotEmpty);
        }
    }
}

using FluentValidation;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Commons;

namespace Shop.Webapp.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageError.NotEmpty);

            RuleFor(x => x.Email).NotEmpty().WithMessage(MessageError.NotEmpty)
                .Must(x => x.ValidateEmail()).WithMessage(MessageError.UnValid);

            RuleFor(x => x.UserName).NotEmpty().WithMessage(MessageError.NotEmpty);

            RuleFor(x => x.Phone)
                .Must(x => string.IsNullOrEmpty(x) || x.ValidatePhoneNumber())
                .WithMessage(MessageError.UnValid);

            RuleFor(x => x.Password).NotEmpty().WithMessage(MessageError.NotEmpty);
        }
    }

    public class UpdatUserValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdatUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageError.NotEmpty);

            RuleFor(x => x.Email).NotEmpty().WithMessage(MessageError.NotEmpty)
                .Must(x => x.ValidateEmail()).WithMessage(MessageError.UnValid);

            RuleFor(x => x.Phone)
                .Must(x => string.IsNullOrEmpty(x) || x.ValidatePhoneNumber())
                .WithMessage(MessageError.UnValid);


        }
    }
}

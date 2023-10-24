using FluentValidation;
using Shop.Webapp.Application.Auth.Models;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Commons;

namespace Shop.Webapp.Application.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().WithMessage(MessageError.NotEmpty);
            RuleFor(_ => _.Email).NotEmpty().WithMessage(MessageError.NotEmpty)
                .Must(_ => _.ValidateEmail()).WithMessage(MessageError.UnValid);
            RuleFor(_ => _.Phone)
                .Must(_ => string.IsNullOrEmpty(_) || _.ValidatePhoneNumber())
                .WithMessage(MessageError.UnValid);
            RuleFor(_ => new { _.Password, _.ConfirmPassword })
                .Must(_ => string.IsNullOrEmpty(_.Password) || string.IsNullOrEmpty(_.ConfirmPassword))
                .WithMessage(MessageError.NotEmpty)
                .Must(_ => _.Password == _.ConfirmPassword).WithMessage(MessageError.UnValid);
        }
    }
}

using AutoMapper;
using FluentValidation.Results;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application
{
    public abstract class AppService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ICurrentUser CurrentUser;
        protected readonly IMapper Mapper;

        public AppService(IUnitOfWork unitOfWork, ICurrentUser currentUser, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            CurrentUser = currentUser;
            Mapper = mapper;
        }

        protected void ThrowValidate(List<ValidationFailure> failures)
        {
            var errors = new Dictionary<string, string>();
            foreach (var item in failures)
                errors.Add(item.PropertyName, item.ErrorMessage);

            throw new CustomerException("ErrorValidate", errors);
        }

        protected void ThrowModelError(string key, string messageCode)
        {
            var errors = new Dictionary<string, string>();
            errors.Add(key, messageCode);
            throw new CustomerException("ErrorValidate", errors);
        }
    }
}

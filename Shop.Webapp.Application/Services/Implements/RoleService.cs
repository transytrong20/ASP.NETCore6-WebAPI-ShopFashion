using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ApiModels.CheckIfNull;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Services.Implements
{
    public class RoleService : AppService, IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly AddRoleValidators _roleValidator;
        public RoleService(
            AddRoleValidators roleValidator,
            IRepository<Role> roleRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper) : base(unitOfWork, currentUser, mapper)

        {
            _roleRepository = roleRepository;
            _roleValidator = roleValidator;
        }

        public async Task<RoleDto> AddAsync(AddRoleModel model)
        {
            var valid = _roleValidator.Validate(model);
            if (!valid.IsValid)
                ThrowValidate(valid.Errors);

            if (_roleRepository.AsNoTracking().Any(_ => _.Name.ToLower() == model.Name.ToLower()))
                ThrowModelError(nameof(model.Name), MessageError.Dupplicate);

            var isNameRole = await _roleRepository.AsNoTracking().AnyAsync(x => x.Name == model.Name);
            if (isNameRole)
                throw new CustomerException($"tên Role {model.Name} đã được sử dụng");

            var role = Mapper.Map<Role>(model);

            await UnitOfWork.BeginTransactionAsync();
            await _roleRepository.InsertAsync(role);
            await UnitOfWork.SaveChangesAsync();
            return Mapper.Map<RoleDto>(role);
        }

        public async Task<GenericPagingResult<RoleDto>> GetAllAsync(RolePagingFilter filter)
        {
            var query = _roleRepository.AsNoTracking();

            var total = query.Count();
            var paged = query.OrderBy(filter).PageBy(filter).ToArray();
            var data = paged.Select(x =>
            {
                return new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsDefault = x.IsDefault,
                    Static = x.Static,
                    Description = x.Description,
                };
            });

            return new GenericPagingResult<RoleDto>(total, data, filter);
        }

        public async Task<RoleDto> GetByIdAsync(Guid Id)
        {

            var role = await _roleRepository.FindAsync(Id);
            if (role == null)
                throw new Exception("không tìm thấy role");
            var result = Mapper.Map<RoleDto>(role);
            return result;
        }

        public async Task<GenericActionResult> RemoveAsync(Guid id)
        {
            var role = await _roleRepository.FindAsync(id);
            if (role == null)
                throw new NotFoundException(MessageError.NotFound);
            await UnitOfWork.BeginTransactionAsync();
            await _roleRepository.DeleteAsync(role);
            await UnitOfWork.SaveChangesAsync();
            return new GenericActionResult(ActionMessage.Remove);
        }

        public async Task<RoleDto> UpdateAsync(Guid id, AddRoleModel model)
        {
            var role = await _roleRepository.FindAsync(id);

            if (role == null)
                throw new Exception("Không tìm thấy role");

            var isNameRole = await _roleRepository.AsNoTracking().AnyAsync(x => x.Name == model.Name && x.Id != id);
            if (isNameRole)
                throw new CustomerException($"tên Role {role.Name} đã được sử dụng");

            ModelCheckIfNull<AddRoleModel, Role>.CheckIfNull(model, role);

            Mapper.Map(model, role);
            role.Name = model.Name;
            role.IsDefault = model.IsDefault.Value;
            role.Static = model.Static.Value;
            role.Description = model.Description;

            await UnitOfWork.BeginTransactionAsync();
            await _roleRepository.UpdateAsync(role);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<RoleDto>(role);
        }
    }
}

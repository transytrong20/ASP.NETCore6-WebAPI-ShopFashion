using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Auth.Abstracts;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.Helpers;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ApiModels.CheckIfNull;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.Commons;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Services.Implements
{
    public class UserService : AppService, IUserService
    {
        private readonly CreateUserValidator _userValidator;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IUserManager _userManager;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly UpdatUserValidator _updateValidator;
        public UserService(
            CreateUserValidator userValidator,
            IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper,
            IUserManager userManager,
            IRepository<UserRole> userRoleRepository,
            UpdatUserValidator updateValidator
            ) : base(unitOfWork, currentUser, mapper)
        {
            _userValidator = userValidator;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
            _updateValidator = updateValidator;
        }


        public async Task<UserDto> CreateAsync(CreateUserModel model)
        {

            var valid = _userValidator.Validate(model);
            if (!valid.IsValid)
                ThrowValidate(valid.Errors);

            var pwdValid = model.Password.ValidatePassword();
            if (!string.IsNullOrEmpty(pwdValid))
                ThrowModelError(nameof(model.Password), MessageError.PwdIsNotValid);

            if (!_roleRepository.AsNoTracking().Any(_ => model.RoleId == _.Id))
                ThrowModelError(nameof(model.RoleId), MessageError.DataNotFound);

            if (_userRepository.AsNoTracking().Any(_ => model.Email == _.Email))
                ThrowModelError(nameof(model.Email), "Email already exists");
            if (_userRepository.AsNoTracking().Any(_ => model.UserName == _.Username))
                ThrowModelError(nameof(model.UserName), "User name already exists");
            if (_userRepository.AsNoTracking().Any(_ => model.Phone == _.Phone))
                ThrowModelError(nameof(model.Phone), "Phone already exists");

            var user = Mapper.Map<User>(model);
            user = await _userManager.CreateAsync(new User()
            {
                SurName = model.SurName,
                Username = model.UserName,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Avatar = ApplicationConsts.AvatarDefault,
                HashCode = Guid.NewGuid().ToString()
            }, model.Password);
            var roleId = _roleRepository.AsNoTracking().FirstOrDefault(x => x.Id == model.RoleId);
            await _userRoleRepository.InsertAsync(new UserRole() { RoleId = roleId.Id, UserId = user.Id });

            await UnitOfWork.BeginTransactionAsync();
            await UnitOfWork.SaveChangesAsync();
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserModel model)
        {
            //var valid = _updateValidator.Validate(model);
            //if (!valid.IsValid)
            //    ThrowValidate(valid.Errors);

            var user = await _userRepository.FindAsync(id);
            if (user == null)
                throw new Exception("User account not found");

            var isEmailUsed = await _userRepository.AsNoTracking().AnyAsync(x => x.Email == model.Email && x.Id != id);
            if (isEmailUsed)
                throw new CustomerException($"Email {user.Email} has been used");

            var isPhoneUsed = await _userRepository.AsNoTracking().AnyAsync(x => x.Phone == model.Phone && x.Id != id);
            if (!string.IsNullOrEmpty(user.Phone) && isPhoneUsed)
                throw new CustomerException($"Phone number {user.Phone} has been used");

            ModelCheckIfNull<UpdateUserModel, User>.CheckIfNull(model, user);

            user.SurName = model.SurName;
            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;


            await UnitOfWork.BeginTransactionAsync();
            await _userManager.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {

            var user = await _userRepository.FindAsync(id);
            if (user == null)
                throw new Exception("User account not found");

            var result = Mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<GenericPagingResult<UserDto>> GetPagingAsync(UserPagingFilter filter)
        {
            var query = _userRepository.AsNoTracking()
                .Include(x => x.Roles).ThenInclude(x => x.Role).AsQueryable();
            if (filter.RoleId.HasValue)
                query = query.Where(x => x.Roles.Any(c => c.RoleId == filter.RoleId));

            var total = query.Count();
            var paged = query.OrderBy(filter).PageBy(filter).ToArray();
            var data = paged.Select(x =>
            {
                return new UserDto
                {
                    Id = x.Id,
                    SurName = x.SurName,
                    Name = x.Name,
                    Username = x.Username,
                    Email = x.Email,
                    EmailVerified = x.EmailVerified,
                    EmailVerifiedTime = x.EmailVerifiedTime,
                    Phone = x.Phone,
                    PhoneVerified = x.PhoneVerified,
                    PhoneVerifiedTime = x.PhoneVerifiedTime,
                    Avatar = x.Avatar,
                    AllowLockUser = x.AllowLockUser,
                    IsLocked = x.IsLocked,
                    RoleId = x.Roles.Select(x => x.RoleId).ToArray(),
                    RoleName = x.Roles.Select(c => c.Role.Name).ToArray(),
                };
            });

            return new GenericPagingResult<UserDto>(total, data, filter);
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await _userRepository.FindAsync(id);
            if (user == null)
                throw new NotFoundException(MessageError.NotFound);
            await UnitOfWork.BeginTransactionAsync();

            await _userRoleRepository.DeleteDirectAsync(x => x.UserId == id);
            await _userRepository.DeleteAsync(user);

            await UnitOfWork.SaveChangesAsync();

            FileHelper.RemoveFile(user.Avatar);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string newPassword, string conFirm)
        {
            var user = await _userRepository.FindAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found.");

            var pwdValid = newPassword.ValidatePassword();
            if (!string.IsNullOrEmpty(pwdValid))
                ThrowModelError(nameof(newPassword), MessageError.PwdIsNotValid);

            string newHashedPassword = newPassword.GetPasswordHash(newPassword);
            user.PasswordHash = newHashedPassword;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}

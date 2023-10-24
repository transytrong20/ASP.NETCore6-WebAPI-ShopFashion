using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Auth.Abstracts;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.Commons;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Auth.Implements
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;

        public UserManager(IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public IQueryable<User> Users => _userRepository.GetQueryable();

        public async Task AddRoleToUserAsync(User user, string roleName)
        {
            var role = await _roleRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
                throw new CustomerException($"Không tìm thấy vai trò có tên là {roleName}");

            if (await _userRoleRepository.AsNoTracking().AnyAsync(x => x.UserId == user.Id && x.RoleId == role.Id))
                throw new CustomerException($"Người dùng {user.Name} đã có vai trò là {roleName}");

            await _userRoleRepository.InsertAsync(new UserRole() { RoleId = role.Id, UserId = user.Id });
        }

        public async Task<User> CreateAsync(User user, string password)
        {
            if (await CheckExistUserAsync(user.Username))
                throw new CustomerException($"tên đăng nhập '{user.Username}' đã được sử dụng");
            if (await CheckEmailUsedAsync(user.Email))
                throw new CustomerException($"Email {user.Email} đã được sử dụng");
            if (!string.IsNullOrEmpty(user.Phone) && await CheckPhoneUsedAsync(user.Phone))
                throw new CustomerException($"Số điện thoại đã được sử dụng");

            user.PasswordHash = password.GetPasswordHash(user.HashCode);
            await _userRepository.InsertAsync(user);
            return user;
        }

        public Task<User> FindAsync(Guid id)
        {
            return _userRepository.FindAsync(id);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _userRepository.GetQueryable()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> FindByNameAsync(string username)
        {
            return _userRepository.GetQueryable()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public Task<string[]> GetRoleByUserAsync(User user)
        {
            var roles = _userRoleRepository.AsNoTracking()
                .Include(x => x.Role).Where(x => x.UserId == user.Id).Select(x => x.Role.Name);
            return roles.ToArrayAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var role = await _roleRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
                throw new CustomerException($"Không tìm thấy vai trò có tên là {roleName}");

            return await _userRoleRepository.AsNoTracking()
                .AnyAsync(x => x.UserId == user.Id && x.RoleId == role.Id);
        }

        public async Task RemoveAsync(User user)
        {
            await _userRoleRepository.DeleteDirectAsync(x => x.UserId == user.Id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task RemoveRoleToUserAsync(User user, string roleName)
        {
            var role = await _roleRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
                throw new CustomerException($"Không tìm thấy vai trò có tên là {roleName}");

            var userRole = await _userRoleRepository.FindAsync(new { UserId = user.Id, RoleId = role.Id });
            if (userRole == null)
                throw new CustomerException($"Tài khoản của người dùng {user.Name} đang không chứa vai trò {roleName}");

            await _userRoleRepository.DeleteAsync(userRole);
        }

        public async Task<User> UpdateAsync(User user)
        {
            if (_userRepository.IsTrackedEntity(user))
                return await _userRepository.UpdateAsync(user);

            var userUpdate = await _userRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == user.Id);
            if (userUpdate == null)
                throw new Exception("Không tìm thấy tài khoản người dùng");

            userUpdate.SurName = user.SurName;
            userUpdate.Name = user.Name;
            userUpdate.HashCode = user.HashCode;
            userUpdate.PasswordHash = user.PasswordHash;
            if (userUpdate.Phone != user.Phone)
            {
                userUpdate.Phone = user.Phone;
                userUpdate.PhoneVerified = false;
                userUpdate.PhoneVerifiedTime = null;
            }
            if (userUpdate.Email != user.Email)
            {
                userUpdate.Email = user.Email;
                userUpdate.EmailVerified = false;
                userUpdate.EmailVerifiedTime = null;
            }
            userUpdate.AllowLockUser = user.AllowLockUser;
            userUpdate.IsLocked = user.IsLocked;
            userUpdate.AccessFailCount = user.AccessFailCount;
            userUpdate.EndLockedTime = user.EndLockedTime;
            return await _userRepository.UpdateAsync(user);
        }

        public Task<bool> CheckPhoneUsedAsync(string phone)
        {
            return _userRepository.AsNoTracking()
                .AnyAsync(x => x.Phone == phone);
        }

        public Task<bool> CheckEmailUsedAsync(string email)
        {
            return _userRepository.AsNoTracking()
                .AnyAsync(x => x.Email == email);
        }

        public Task<bool> CheckExistUserAsync(string username)
        {
            return _userRepository.AsNoTracking()
                .AnyAsync(x => x.Username == username || x.Email == username || x.Phone == username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var passwordHash = password.GetPasswordHash(user.HashCode);
            if (passwordHash == user.PasswordHash)
                return true;

            if (!user.AllowLockUser)
                return false;

            user.AccessFailCount++;
            if (user.AccessFailCount == 5)
            {
                await LockedUserAsync(user, 5);
            }
            return false;
        }

        public async Task<bool> CheckAllowLoginAsync(User user)
        {
            bool result = false;
            if (!user.AllowLockUser || !user.IsLocked)
                result = true;

            if (user.IsLocked && !user.EndLockedTime.HasValue)
                result = false;

            if (user.EndLockedTime.HasValue && user.EndLockedTime > DateTime.Now)
                result = true;

            if (true)
            {
                user.IsLocked = false;
                user.AccessFailCount = 0;
                user.EndLockedTime = null;

                await UpdateAsync(user);
            }
            return result;
        }

        public async Task LockedUserAsync(User user, int minutes)
        {
            if (!user.AllowLockUser)
                throw new CustomerException($"Tài khoản người dùng này không thể bị khóa");

            if (minutes <= 0)
                throw new CustomerException($"Thời gian khóa tài khoản không hợp lệ");

            user.IsLocked = true;
            user.EndLockedTime = DateTime.Now.AddMinutes(minutes);
            await UpdateAsync(user);
        }

        public async Task LockedUserAsync(User user)
        {
            if (!user.AllowLockUser)
                throw new CustomerException($"Tài khoản người dùng này không thể bị khóa");

            user.IsLocked = true;
            user.AccessFailCount = 0;
            await UpdateAsync(user);
        }
    }
}

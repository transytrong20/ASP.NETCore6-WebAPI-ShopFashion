using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Auth.Abstracts;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Auth.Implements
{
    public class RoleManager : IRoleManager
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleManager(IRepository<Role> repository)
        {
            _roleRepository = repository;
        }

        public IQueryable<Role> Roles => _roleRepository.GetQueryable();

        public async Task<Role> CreateAsync(Role role)
        {
            if (await _roleRepository.AsNoTracking().AnyAsync(x => x.Name == role.Name))
                throw new CustomerException($"Tên vai trò {role.Name} đã được sử dụng");

            return await _roleRepository.InsertAsync(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
                throw new CustomerException($"Không tìm thấy vai trò có id là {id}");

            await _roleRepository.DeleteAsync(role);
        }

        public Task DeleteAsync(Role role)
        {
            return _roleRepository.DeleteAsync(role);
        }

        public Task<Role> FindAsync(Guid id)
        {
            return _roleRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            if (await _roleRepository.AsNoTracking().AnyAsync(x => x.Name == role.Name && x.Id != role.Id))
                throw new CustomerException($"Tên vai trò {role.Name} đã được sử dụng");

            var roleUpdate = await _roleRepository.FindAsync(role.Id);

            if (roleUpdate.Static)
                throw new CustomerException($"Vai trò {role.Name} không cho phép chỉnh sửa");

            if (_roleRepository.IsTrackedEntity(role))
            {
                return await _roleRepository.UpdateAsync(role);
            }
            else
            {
                roleUpdate.Name = role.Name;
                roleUpdate.IsDefault = role.IsDefault;
                roleUpdate.Description = role.Description;

                return await _roleRepository.UpdateAsync(roleUpdate);
            }
        }
    }
}

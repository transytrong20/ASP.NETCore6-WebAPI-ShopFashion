using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ApiModels.Results;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface IRoleService
    {
        Task<RoleDto> AddAsync(AddRoleModel model);
        Task<RoleDto> UpdateAsync(Guid id, AddRoleModel model);
        Task<RoleDto> GetByIdAsync(Guid id);
        Task<GenericPagingResult<RoleDto>> GetAllAsync(RolePagingFilter filter);
        Task<GenericActionResult> RemoveAsync(Guid id);
    }
}

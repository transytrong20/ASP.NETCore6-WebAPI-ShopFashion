using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ApiModels.Results;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserModel model);

        Task<UserDto> UpdateAsync(Guid id, UpdateUserModel model);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<GenericPagingResult<UserDto>> GetPagingAsync(UserPagingFilter filter);
        Task RemoveAsync(Guid id);
        Task<bool> ChangePasswordAsync(Guid userId, string newPassword, string conFirm);
    }
}

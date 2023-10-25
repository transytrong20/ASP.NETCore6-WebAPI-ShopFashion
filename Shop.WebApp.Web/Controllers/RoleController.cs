using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route($"{ApplicationConsts.FirstRoute}/role")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(
            IRoleService roleService
            )
        {
            _roleService = roleService;
        }

        [HttpPost("add-role")]
        public async Task<RoleDto> AddAsync([FromForm] AddRoleModel model)
        {
            return await _roleService.AddAsync(model);
        }

        [HttpPut("{id}/update")]
        public async Task<RoleDto> UpdateAsync(Guid id, [FromForm] AddRoleModel model)
        {
            return await _roleService.UpdateAsync(id, model);
        }

        [HttpGet("get/{id}")]
        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            return await _roleService.GetByIdAsync(id);
        }

        [HttpGet("get-all")]
        public async Task<GenericPagingResult<RoleDto>> GetAllAsync([FromQuery] RolePagingFilter filter)
        {
            return await _roleService.GetAllAsync(filter);
        }

        [HttpDelete("{id}/remove")]
        public async Task<GenericActionResult> RemoveAsync(Guid id)
        {
            var result = await _roleService.RemoveAsync(id);
            return result;
        }
    }
}

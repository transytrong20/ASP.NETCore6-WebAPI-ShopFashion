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
    [Route($"{ApplicationConsts.FirstRoute}/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<UserDto> CreateUser([FromBody] CreateUserModel model)
        {
            var result = await _userService.CreateAsync(model);
            return result;
        }


        [HttpPut("{id}/update")]
        public async Task<UserDto> UpdateAsync(Guid id, [FromForm] UpdateUserModel model)
        {
            var result = await _userService.UpdateAsync(id, model);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("get/{id}")]
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("paging")]
        public async Task<GenericPagingResult<UserDto>> GetPagingAsync([FromQuery] UserPagingFilter filter)
        {
            return await _userService.GetPagingAsync(filter);
        }

        [HttpDelete("{id}/remove")]
        public async Task<GenericActionResult> RemoveAsync(Guid id)
        {
            await _userService.RemoveAsync(id);
            return new GenericActionResult(ActionMessage.Remove);
        }

        [HttpPut("{userId}/changepassword")]
        public async Task<bool> ChangePassword(Guid userId, string newPassword, string conFirm)
        {
            var result = await _userService.ChangePasswordAsync(userId, newPassword, conFirm);
            return result;
        }
    }
}

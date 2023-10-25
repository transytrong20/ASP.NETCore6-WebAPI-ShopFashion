using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.Email;
using Shop.Webapp.Application.Email.Model;
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
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
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

        [AllowAnonymous]
        [HttpPost("send-reset-email{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var result = await _emailService.SendEmailAsync(email);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel reset)
        {
            var result = await _emailService.ResetPasswordAsync(reset);
            return result;
        }
    }
}

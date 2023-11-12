using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.Email;
using Shop.Webapp.Application.Email.Model;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels.ResponseMessage;
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
        private readonly IRepository<User> _userRepository;
        public UserController(IUserService userService, IEmailService emailService, IRepository<User> userRepository)
        {
            _userService = userService;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<MessageSuccess<UserDto>> CreateUser([FromBody] CreateUserModel model)
        {
            var result = await _userService.CreateAsync(model);

            if (result != null)
            {
                return new MessageSuccess<UserDto>
                {
                    Success = true,
                    Data = result,
                    Message = "User created successfully."
                };
            }
            return new MessageSuccess<UserDto>
            {
                Success = false,
                Data = null,
                Message = "User creation failed."
            };
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

        

        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            var role = User.FindFirst(CustomeClaimTypes.Role)?.Value;
            var username = User.FindFirst(CustomeClaimTypes.Username)?.Value;

            var userInfo = _userRepository.AsNoTracking().Where(x => x.Username == username).Include(x => x.Roles).ThenInclude(ur => ur.Role).Select(x => new
            {
                x.Name,
                x.Username,
                x.Email,
                x.Phone,
                Role = x.Roles.Select(ur => ur.Role.Name).FirstOrDefault(),
            }).FirstOrDefault();
            if (userInfo == null)
            {
                return NotFound("User information not found.");
            }
            return Ok(userInfo);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Auth;
using Shop.Webapp.Application.Auth.Models;
using Shop.Webapp.Shared.ApiModels.ResponseMessage;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Controllers
{
    [ApiController]
    [Route($"{ApplicationConsts.FirstRoute}/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<MessageSuccess<TokenResult>> Login([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);
            if (result != null)
            {
                return new MessageSuccess<TokenResult>
                {
                    Success = true,
                    Data = result,
                    Message = "User login successfully."
                };
            }
            return new MessageSuccess<TokenResult>
            {
                Success = false,
                Data = null,
                Message = "User login failed."
            };
        }

        [Authorize]
        [HttpGet("loginv3")]
        public IActionResult Loginv12(string model)
        {
            return Ok(new { model });
        }

        [HttpPost("register")]
        public async Task<GenericActionResult> Register([FromBody] RegisterModel model)
        {
            await _authService.RegisterAsync(model);
            return new GenericActionResult("Auth.Register.Success");
        }
    }


}

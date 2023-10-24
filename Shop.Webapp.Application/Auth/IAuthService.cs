using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shop.Webapp.Application.Auth.Abstracts;
using Shop.Webapp.Application.Auth.Models;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.Domain;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Webapp.Application.Auth
{
    public interface IAuthService
    {
        Task<TokenResult> LoginAsync(LoginModel model);
        Task RegisterAsync(RegisterModel model);
    }
    public class AuthService : IAuthService
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly int expirySeconds;
        private readonly IUserManager _userManager;
        private readonly RegisterValidator _validator;

        public AuthService(IUserManager userManager, IConfiguration configuration, RegisterValidator validator)
        {
            _userManager = userManager;
            _validator = validator;
            var section = configuration.GetSection("TokenSetting");
            secretKey = section["SecretKey"];
            issuer = section["Issuer"];
            expirySeconds = Int32.Parse(section["TokenExpiry"]) * 60;
        }

        public async Task<TokenResult> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.Username);

            if (user == null)
                throw new CustomerException("Thông tin tài khoản hoặc mật khẩu không chính xác!");

            if (!await _userManager.CheckAllowLoginAsync(user))
                throw new CustomerException("Tài khoản của bạn đang bị khóa, vui lòng thử lại sau!");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (user.IsLocked)
                    throw new CustomerException("Thông tin tài khoản hoặc mật khẩu không chính xác");
            }

            var roles = await _userManager.GetRoleByUserAsync(user);

            var now = DateTime.Now;
            var claims = new Claim[]
            {
                new Claim(CustomeClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomeClaimTypes.SurName, string.IsNullOrEmpty(user.SurName) ? string.Empty : user.SurName),
                new Claim(CustomeClaimTypes.Name, user.Name),
                new Claim(CustomeClaimTypes.Username, user.Username),
                new Claim(CustomeClaimTypes.Email, user.Email),
                new Claim(CustomeClaimTypes.PhoneNumber, string.IsNullOrEmpty(user.Phone) ? string.Empty : user.Phone),
                new Claim(CustomeClaimTypes.Role, JsonConvert.SerializeObject(roles != null ? roles : new string[0]))
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256);
            var expiry = now.AddSeconds(expirySeconds);

            var jwt = CreateSecurityToken(issuer, claims, now, expiry, signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResult { UserId = user.Id, Token = token, Username = user.Username };
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var validate = _validator.Validate(model);
            if (!validate.IsValid)
                throw new CustomerException(validate.Errors.FirstOrDefault().ErrorMessage);

            var user = await _userManager.CreateAsync(new User()
            {
                SurName = model.SurName,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Username = model.Email,
                Avatar = ApplicationConsts.AvatarDefault,
                HashCode = Guid.NewGuid().ToString()
            }, model.Password);
        }

        private JwtSecurityToken CreateSecurityToken(string issuer, IEnumerable<Claim> claims, DateTime now, DateTime expiry, SigningCredentials credentials)
            => new(claims: claims, notBefore: now, expires: expiry, issuer: issuer, signingCredentials: credentials);
    }

}

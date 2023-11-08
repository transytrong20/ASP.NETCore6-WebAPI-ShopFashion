using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Webapp.Application.Email.Abstracts;
using Shop.Webapp.Application.Email.Helpers;
using Shop.Webapp.Application.Email.Model;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.EFcore.Repositories.Impls;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ApiModels.Requests;
using Shop.Webapp.Shared.Exceptions;
using System.Security.Cryptography;

namespace Shop.Webapp.Application.Email
{
    public interface IEmailService
    {
        Task<IActionResult> SendEmailAsync(string email);
        Task<IActionResult> ResetPasswordAsync(ResetPasswordModel reset);
    }

    public class EmailService : AppService, IEmailService
    {
        private readonly ISendMailService _sendEmailService;
        private readonly IRepository<User> _userRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public EmailService(IRepository<User> userRepository,
            IConfiguration configuration,
            ISendMailService sendEmailService,
            AppDbContext appDbContext,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper) : base(unitOfWork, currentUser, mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _sendEmailService = sendEmailService;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> SendEmailAsync(string email)
        {
            var user = await _userRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            if (user is null)
                throw new NotFoundException("Email doesn't Exist");
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            string from = _configuration.GetSection("From").Value;
            var emailModel = new EmailModel(email, "Reset Password", EmailBody.EmailStringBody(_configuration, email, emailToken));
            _sendEmailService.SendEmailAsync(emailModel);
            _appDbContext.Entry(user).State = EntityState.Modified;
            await _userRepository.SaveChangesAsync();
            return new OkObjectResult(new GenericOkResult<User>(200, "Success", user));
        }

        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel reset)
        {
            var newToken = reset.EmailToken.Replace(" ", "+");
            var user = await _userRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Email == reset.Email);
            if (user is null)
                throw new NotFoundException("Email doesn't Exist");
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != reset.EmailToken || emailTokenExpiry < DateTime.Now)
                throw new NotFoundException("Invalid Reset link!");
            _appDbContext.Entry(user).State = EntityState.Modified;
            _appDbContext.SaveChangesAsync();
            return new OkObjectResult(new GenericOkResult<User>(200, "Password Reset Successfully", user));
        }
    }
}

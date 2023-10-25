using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Shop.Webapp.Application.Email.Abstracts;
using Shop.Webapp.Application.Email.Model;

namespace Shop.Webapp.Application.Email.Implements
{
    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration _config;
        public SendMailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void SendEmailAsync(EmailModel request)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            emailMessage.To.Add(MailboxAddress.Parse(request.To));
            emailMessage.Subject = request.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format(request.Content)
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }


    }
}

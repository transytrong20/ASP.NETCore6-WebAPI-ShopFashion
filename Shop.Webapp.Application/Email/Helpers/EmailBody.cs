using Microsoft.Extensions.Configuration;

namespace Shop.Webapp.Application.Email.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(IConfiguration configuration, string email, string emailToken)
        {
            string LinkSendEmail = configuration.GetSection("SendEmail").Value;
            return $@"<html>
<head>
</head>
<body style=""margin:0;padding:0;font-family: Arial, Helvetica, sans-serif;"">
    <div style=""height: auto;background: linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat;width:400px;padding:30px;color: white"">
        <div>
            <div>
                <h1> Reset your Password </h1>
                <hr>
                <p>You're receiving this e-mail because you requested a password reset for your signin account. </p>
                <p>Please tap the button below to choose a new password.</p>
                <a href=""{LinkSendEmail}{email}&code={emailToken}"" target=""_blank"" style=""Background:#0d6efd;padding:10px;border:none;color:white;border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none;""> Reset Password</a><br>
                
                <p>Kind Regards,<br><br>
                Let's program</p>
            </div>
        </div>
    </div>
</body>
</html>
";
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shop.WebApp.Web.Infrastructures.Configurations
{
    public static class JWTAuthenticationConfiguration
    {
        public static void JWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var tokensSection = configuration.GetSection("TokenSetting");
            var secretKey = Encoding.UTF8.GetBytes(tokensSection["SecretKey"]);
            var issuer = tokensSection["Issuer"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrEmpty(context.Token))
                            context.Token = context.Request.Cookies["shop-authentication"];
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}

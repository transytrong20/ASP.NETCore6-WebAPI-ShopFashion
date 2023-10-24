using Microsoft.OpenApi.Models;
using Shop.WebApp.Web.Infrastructures.Commons;

namespace Shop.WebApp.Web.Infrastructures.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void SwaggerServiceConfig(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            var runtime = DateTime.Now;

            builder.Services.AddSwaggerGen(act => {

                act.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shop website",
                    Version = "v1",
                });

                act.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                act.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      Array.Empty<string>()
                    }
                  });

                act.OperationFilter<SwaggerOperationFilter>();
            });
        }
    }
}

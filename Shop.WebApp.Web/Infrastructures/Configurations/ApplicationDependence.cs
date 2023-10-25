using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application;
using Shop.Webapp.Application.Auth;
using Shop.Webapp.Application.Auth.Abstracts;
using Shop.Webapp.Application.Auth.Implements;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Services.Implements;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.EFcore;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.EFcore.Repositories.Impls;
using Shop.Webapp.Shared.ApiModels;
using Shop.WebApp.Web.Infrastructures.Commons;

namespace Shop.WebApp.Web.Infrastructures.Configurations
{
    public static class ApplicationDependence
    {
        public static void AutoRegister(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AppMapperProfileRegister).Assembly);
            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(config.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);
            }, ServiceLifetime.Scoped);

            //Validator
            services.AddSingleton<RegisterValidator>();
            services.AddSingleton<AddRoleValidators>();
            services.AddSingleton<CreateUserValidator>();
            services.AddSingleton<UpdatUserValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
            services.AddScoped(typeof(IBasicRepository<>), typeof(BasicRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

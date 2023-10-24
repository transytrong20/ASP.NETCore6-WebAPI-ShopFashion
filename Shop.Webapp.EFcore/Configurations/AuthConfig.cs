using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Seedings;
using Shop.Webapp.Shared.ConstsDatas;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Webapp.EFcore.Configurations
{
    public static class AuthConfig
    {
        public static void AuthenticationConfiguration([NotNull] this ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable($"{ApplicationConsts.Schema}_{nameof(User).ToLower()}")
                .HasQueryFilter(x => !x.IsDeleted)
                .HasData(AuthSeeding.GetUsers());

            builder.Entity<Role>()
                .ToTable($"{ApplicationConsts.Schema}_{nameof(Role).ToLower()}")
                .HasData(AuthSeeding.GetRoles());

            builder.Entity<UserRole>()
                .ToTable($"{ApplicationConsts.Schema}_{nameof(UserRole).ToLower()}")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<UserRole>()
                .HasData(AuthSeeding.GetUserRoles());

            builder.Entity<PermissionGrant>()
                .ToTable($"{ApplicationConsts.Schema}_{nameof(PermissionGrant).ToLower()}")
                .HasData(AuthSeeding.GetPermissionGrants());
        }
    }
}

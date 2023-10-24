using Shop.Webapp.Domain;
using Shop.Webapp.Shared.Commons;

namespace Shop.Webapp.EFcore.Seedings
{
    public static class AuthSeeding
    {
        public static User[] GetUsers()
        {
            return new User[] {
                new User(){
                    Id = Guid.Parse("49267eb3-4174-4081-a3e0-c57cfc001353"),
                    SurName = "Tài khoản admin", Name = "Admin Manager",
                    HashCode = "49267eb3-4174-4081-a3e0-c57cfc001355",
                    Username = "manager", PasswordHash = "1q2w3E*".GetPasswordHash("49267eb3-4174-4081-a3e0-c57cfc001355"),
                    Email = "manager@gmail.com", EmailVerified = true,
                    AllowLockUser = false
                }
            };
        }

        public static Role[] GetRoles()
        {
            return new Role[] {
                new Role(){
                    Id = Guid.Parse("7299f85a-344e-4045-944b-aba6e4cd58a1"),
                    Name = "manager",
                    IsDefault = false,
                    Static = true,
                    Description = "Quyền admin, quản lý toàn bộ trang web"
                }
            };
        }

        public static UserRole[] GetUserRoles()
        {
            return new UserRole[] {
                new UserRole(){
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("49267eb3-4174-4081-a3e0-c57cfc001353"),
                    RoleId = Guid.Parse("7299f85a-344e-4045-944b-aba6e4cd58a1")
                }
            };
        }

        public static PermissionGrant[] GetPermissionGrants()
        {
            return new PermissionGrant[] { };
        }
    }
}

using Shop.Webapp.Shared.DtoEntities;

namespace Shop.Webapp.Application.Dto
{
    public class UserDto : FullAuditedEntityDto
    {
        public string? SurName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }
        public DateTime? EmailVerifiedTime { get; set; }
        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }
        public DateTime? PhoneVerifiedTime { get; set; }
        public string? Avatar { get; set; }
        public bool AllowLockUser { get; set; }
        public bool IsLocked { get; set; }
        public Guid[]? RoleId { get; set; }
        public string[]? RoleName { get; set; }
    }
}

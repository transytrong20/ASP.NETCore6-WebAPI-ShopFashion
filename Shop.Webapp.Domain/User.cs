using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class User : FullAuditedEntity
    {
        public string? SurName { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string HashCode { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime? EmailVerifiedTime { get; set; }

        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }
        public DateTime? PhoneVerifiedTime { get; set; }

        public string? Avatar { get; set; }
        public bool AllowLockUser { get; set; }
        public bool IsLocked { get; set; }
        public int AccessFailCount { get; set; }
        public DateTime? EndLockedTime { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}

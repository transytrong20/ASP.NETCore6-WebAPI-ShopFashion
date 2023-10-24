using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Webapp.Domain
{
    public class UserRole : Entity
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}

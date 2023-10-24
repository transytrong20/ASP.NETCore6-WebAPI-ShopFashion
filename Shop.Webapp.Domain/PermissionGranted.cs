using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class PermissionGrant : Entity
    {
        [Required]
        public string PermissionName { get; set; }
        [Required]
        public string ProviderName { get; set; } // R or U
        [Required]
        public string ProviderKey { get; set; } // RoleName or UserId
    }
}

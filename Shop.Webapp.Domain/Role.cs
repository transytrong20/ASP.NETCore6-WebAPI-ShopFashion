using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class Role : Entity
    {
        [Required]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool Static { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<UserRole> Users { get; set; }
    }
}

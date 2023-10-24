using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Shared.Audited
{
    public class Entity
    {
        [Required]
        public Guid Id { get; set; }
    }
}

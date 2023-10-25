using Shop.Webapp.Shared.Audited;

namespace Shop.Webapp.Domain
{
    public class Category : FullAuditedEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Index { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}

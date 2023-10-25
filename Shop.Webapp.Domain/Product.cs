using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class Product : FullAuditedEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public int Sold { get; set; } = 0; //đã bán
        public int Discount { get; set; } //giảm giá
        public virtual IEnumerable<CategoryProduct> Categories { get; set; }
    }
}

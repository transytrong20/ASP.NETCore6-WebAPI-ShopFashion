using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class Product : FullAuditedEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public int? Status { get; set; }
        public int? Sold { get; set; } = 0; //đã bán
        public int? Discount { get; set; } //giảm giá
        public int? Index { get; set; }
        public bool? Accepted { get; set; } //duyệt
        public bool? New { get; set; } //sản phẩm mới
        public bool? Sale { get; set; } //sản phẩm giảm giá
        public int? SaleTurn { get; set; } = 0; //lượt bán hàng 
        public virtual IEnumerable<CategoryProduct> Categories { get; set; }
    }
}

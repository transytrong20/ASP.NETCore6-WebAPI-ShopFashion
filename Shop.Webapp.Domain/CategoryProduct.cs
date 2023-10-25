using Shop.Webapp.Shared.Audited;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Webapp.Domain
{
    public class CategoryProduct : Entity
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}

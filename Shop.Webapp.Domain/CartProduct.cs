using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shop.Webapp.Shared.Audited;

namespace Shop.Webapp.Domain
{
    public class CartProduct : Entity
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}

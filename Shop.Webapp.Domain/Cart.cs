using Shop.Webapp.Shared.Audited;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Webapp.Domain
{
    public class Cart : FullAuditedEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get ; set; }


        [ForeignKey(nameof(UserId))]
        public User Users { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Products { get; set; }
    }
}

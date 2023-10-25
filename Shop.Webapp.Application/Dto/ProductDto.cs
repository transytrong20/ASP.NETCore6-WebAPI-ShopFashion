using Microsoft.AspNetCore.Http;
using Shop.Webapp.Shared.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Webapp.Application.Dto
{
    public class ProductDto : FullAuditedEntityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public int? Status { get; set; }
        public int? Discount { get; set; }
        public bool? Accepted { get; set; }
        public int? Index { get; set; }
        public Guid? CategoryId { get; set; }
        public string[] CategoryName { get; set; } = new string[0];
    }
}

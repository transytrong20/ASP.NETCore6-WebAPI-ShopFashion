using Shop.Webapp.Shared.DtoEntities;

namespace Shop.Webapp.Application.Dto
{
    public class CartDto : FullAuditedEntityDto
    {
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; } 
        public int? Quantity { get; set; }
        public string? Size { get; set; }
    }
}

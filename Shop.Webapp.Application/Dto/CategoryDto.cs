using Shop.Webapp.Shared.DtoEntities;

namespace Shop.Webapp.Application.Dto
{
    public class CategoryDto : FullAuditedEntityDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Index { get; set; }
    }
}

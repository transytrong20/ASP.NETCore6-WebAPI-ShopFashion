using Shop.Webapp.Shared.DtoEntities;

namespace Shop.Webapp.Application.Dto
{
    public class RoleDto : FullAuditedEntityDto
    {
        public string? Name { get; set; }
        public bool? IsDefault { get; set; }
        public bool? Static { get; set; }
        public string? Description { get; set; }
    }
}

namespace Shop.Webapp.Shared.DtoEntities
{
    public class FullAuditedEntityDto : EntityDto
    {
        public DateTime CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}

namespace Shop.Webapp.Shared.Audited
{
    public class FullAuditedEntity : Entity
    {
        public DateTime CreatedTime { get; internal set; } = DateTime.Now;
        public Guid? CreatedBy { get; internal set; }
        public DateTime? LastModifiedTime { get; internal set; }
        public Guid? LastModifiedBy { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public DateTime? DeletedTime { get; internal set; }
        public Guid? DeletedBy { get; internal set; }
    }
}

namespace Alpha.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
      public int Id { get; set; }

      public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

      //added to create a more "complete" list of what usually forms the BaseEntity
      public virtual DateTimeOffset? CreationTime { get; set; } = DateTimeOffset.Now;
      public virtual long? CreatorUserId { get; set; }
      public virtual long? DeleterUserId { get; set; }
      public virtual DateTimeOffset? DeletionTime { get; set; }
      public virtual bool IsDeleted { get; set; } = false;
      public virtual DateTimeOffset? LastModificationTime { get; set; }
      public virtual long? LastModifierUserId { get; set; }
    }
}

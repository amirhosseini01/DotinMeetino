namespace Server.Data.Models;

public abstract class AuditableEntity
{
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedDate { get; set; }
    
    //todo: add CreatedBy, ModifiedBy
}
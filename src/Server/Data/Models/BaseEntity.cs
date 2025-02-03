using System.ComponentModel.DataAnnotations;

namespace Server.Data.Models;

public class BaseEntity: AuditableEntity
{
    [Key]
    public virtual int Id { get; set; }
}
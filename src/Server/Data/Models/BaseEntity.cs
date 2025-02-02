using System.ComponentModel.DataAnnotations;

namespace Server.Data.Models;

public class BaseEntity
{
    [Key]
    public virtual int Id { get; set; }
}
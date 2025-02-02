using System.ComponentModel.DataAnnotations;

namespace Server.Data.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
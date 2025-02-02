using System.ComponentModel.DataAnnotations;
using Server.Data.Base;
using Server.Data.Models;

namespace Server.Modules.Meeting.Models;

public class Room: BaseEntity
{
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string Title { get; set; }
    
    public virtual ICollection<Meeting>? Meetings { get; set; }
}
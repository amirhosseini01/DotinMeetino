using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Data.Base;
using Server.Data.Models;
using Server.Modules.Meeting.Enums;

namespace Server.Modules.Meeting.Models;

public class Meeting: BaseEntity
{
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string Title { get; set; }

    [DisplayName("meeting start at")]
    public required DateTimeOffset StartDateTime { get; set; }
    
    [DisplayName("meeting ends at")]
    public required DateTimeOffset EndDateTime { get; set; }

    [DisplayName("room number or skype-url")]
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public string? Room { get; set; }

    [StringLength(DatabaseHelper.DescriptionsStandardLength)]
    public string? Summary { get; set; }

    [Column(TypeName = DatabaseHelper.Nvarchar10)] 
    public MeetingStatus Status { get; set; } = MeetingStatus.Active;
    
    [Column(TypeName = DatabaseHelper.Nvarchar10)] 
    public MeetingType Type { get; set; } = MeetingType.InPerson;
    
    [NotMapped]
    public TimeSpan ElapsedTime => StartDateTime - EndDateTime;

    public virtual ICollection<MeetingMember>? Members { get; set; }
}
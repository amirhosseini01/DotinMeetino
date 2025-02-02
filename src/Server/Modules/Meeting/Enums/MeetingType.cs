using System.ComponentModel.DataAnnotations;

namespace Server.Modules.Meeting.Enums;

public enum MeetingType
{
    Online,
    [Display(Name = "In-person")]
    InPerson
}
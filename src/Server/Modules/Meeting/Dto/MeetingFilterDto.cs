using Server.Modules.Meeting.Enums;

namespace Server.Modules.Meeting.Dto;

public class MeetingFilterDto
{
    public DateTimeOffset? StartDateTime { get; set; }
    public DateTimeOffset? EndDateTime { get; set; }
    public MeetingStatus[]? StatusArr { get; set; }
}
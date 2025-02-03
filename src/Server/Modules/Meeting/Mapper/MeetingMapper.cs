using Riok.Mapperly.Abstractions;
using Server.Modules.Meeting.Dto;

namespace Server.Modules.Meeting.Mapper;

[Mapper]
public partial class MeetingMapper
{
    public partial Models.Meeting MeetingInputToMeeting(MeetingInputDto input);
}
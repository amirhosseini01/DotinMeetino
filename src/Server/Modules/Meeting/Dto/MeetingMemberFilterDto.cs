﻿using Server.Modules.Meeting.Enums;

namespace Server.Modules.Meeting.Dto;

public record MeetingMemberFilterDto
{
    public DateTimeOffset? MeetingStartDateTime { get; set; }
    public DateTimeOffset? MeetingEndDateTime { get; set; }
    public int[]? UserIdArr { get; set; }
    public MeetingStatus[]? MeetingStatus { get; set; }
}
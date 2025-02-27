﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Data.Base;
using Server.Modules.Meeting.Enums;

namespace Server.Modules.Meeting.Dto;

public class MeetingInputDto
{
    [Range(1, int.MaxValue)]
    public int? RoomId { get; set; }
    
    [Column(TypeName = DatabaseHelper.Nvarchar10)] 
    public MeetingStatus Status { get; set; } = MeetingStatus.Active;
    
    [Column(TypeName = DatabaseHelper.Nvarchar10)] 
    public MeetingType Type { get; set; } = MeetingType.InPerson;
    
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string Title { get; set; }

    [DisplayName("meeting start at")]
    public required DateTimeOffset StartDateTime { get; set; }
    
    [DisplayName("meeting ends at")]
    public required DateTimeOffset EndDateTime { get; set; }
    
    [DisplayName("url skype-url/ google-meet")]
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public string? MeetingUrl { get; set; }

    public int[]? MeetingMembers { get; set; }
}

public class MeetingRouteDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int MeetingId { get; set; }
}

public class MeetingResultInputDto
{
    [Required]
    [StringLength(DatabaseHelper.DescriptionsStandardLength)]
    public required string Result { get; set; }
}
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Enums;
using Server.Modules.Meeting.Mapper;
using Server.Modules.Meeting.Models;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Services;

public static class MeetingServices
{
    public static async Task<ActionResult<Models.Meeting>> Create(this IMeetingRepository meetingRepo, IMeetingMemberRepository memberRepo, MeetingInputDto input, CancellationToken ct = default)
    {
        if (input.MeetingMembers is null || input.MeetingMembers.Length <= 1)
        {
            return new BadRequestObjectResult(Messages.SelectMoreMember);
        }

        if (input.EndDateTime < input.StartDateTime)
        {
            return new BadRequestObjectResult(Messages.EndTimeShouldBiggerThanStart);
        }

        if (input.Type == MeetingType.InPerson && input.RoomId is null)
        {
            return new BadRequestObjectResult(Messages.RoomRequiredForInPersonMeeting);
        }
        
        if (input.Type == MeetingType.Online && string.IsNullOrEmpty(input.MeetingUrl))
        {
            return new BadRequestObjectResult(Messages.UrlRequiredForOnlineMeeting);
        }
        
        var mapper = new MeetingMapper();
        var meeting = mapper.MeetingInputToMeeting(input);
        meeting.Members = input.MeetingMembers.Select(x => new MeetingMember
        {
            UserId = x
        }).ToList();

        if (await meetingRepo.HasOverLap(meeting: meeting))
        {
            return new BadRequestObjectResult(Messages.MeetingHasOverLap);
        }

        var membersWithOverlap = await memberRepo.GetByFilter(filter: new MeetingMemberFilterDto
        {
            MeetingStartDateTime = input.StartDateTime,
            MeetingEndDateTime = input.EndDateTime,
            UserIdArr = input.MeetingMembers,
            MeetingStatus = [MeetingStatus.Active]
        }, ct);
        
        if (membersWithOverlap.Count > 0)
        {
            var sbError = new StringBuilder();
            foreach (var member in membersWithOverlap)
            {
                sbError.AppendLine($"{member.User!.FullName}: is not available for this meeting! plz try another person or time for meeting");
            }
            return new BadRequestObjectResult(sbError.ToString());
        }
        
        
        await meetingRepo.AddAsync(meeting, ct);
        await meetingRepo.SaveChangesAsync(ct);
        return meeting;
    }
    
    public static async Task<ActionResult<Models.Meeting>> Cancel(this IMeetingRepository meetingRepo, MeetingRouteDto route, CancellationToken ct = default)
    {
        var meeting = await meetingRepo.FindAsync(route.MeetingId, ct);
        if (meeting is null)
        {
            return new NotFoundResult();
        }

        meeting.ModifiedDate = DateTimeOffset.UtcNow;
        meeting.Status = MeetingStatus.Canceled;
        
        return meeting;
    }
    
    public static async Task<ActionResult<Models.Meeting>> SubmitResult(this IMeetingRepository meetingRepo, MeetingRouteDto route, MeetingResultInputDto input, CancellationToken ct = default)
    {
        var meeting = await meetingRepo.FindAsync(route.MeetingId, ct);
        if (meeting is null)
        {
            return new NotFoundResult();
        }

        meeting.ModifiedDate = DateTimeOffset.UtcNow;
        meeting.Result = input.Result;
        
        return meeting;
    }
}
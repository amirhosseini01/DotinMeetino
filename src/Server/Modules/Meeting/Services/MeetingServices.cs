using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Modules.Meeting.Dto;
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
            UserIdArr = input.MeetingMembers

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
}
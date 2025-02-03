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
        var validateInputRes = input.ValidateInput();
        if (!validateInputRes)
        {
            return new BadRequestObjectResult(validateInputRes);
        }

        var mapper = new MeetingMapper();
        var meeting = mapper.InputToEntity(input);
        meeting.Members = input.MeetingMembers!.Select(x => new MeetingMember
        {
            UserId = x
        }).ToList();

        var overlapValidation = await meetingRepo.ValidateOverlapValidation(memberRepo: memberRepo, input: input, meeting: meeting, ct: ct);
        if (overlapValidation)
        {
            return new BadRequestObjectResult(validateInputRes);
        }

        await meetingRepo.AddAsync(meeting, ct);
        await meetingRepo.SaveChangesAsync(ct);
        return meeting;
    }
    
    public static async Task<ActionResult<Models.Meeting>> Create(this IMeetingRepository meetingRepo, IMeetingMemberRepository memberRepo, MeetingIntelligenceInputDto input, CancellationToken ct = default)
    {
        var candidateStart = await meetingRepo.SuggestBestStartDateTime(input: input, ct: ct);
        if (candidateStart is null)
        {
            return new BadRequestObjectResult(ResponseBase.Failed<Models.Meeting>(Messages.NoSuitableTimeFindInTheNextWeek));
        }
        
        var mapper = new MeetingMapper();
        var newInput = mapper.InputToIntelligenceInput(input);
        
        var validateInputRes = newInput.ValidateInput();
        if (!validateInputRes)
        {
            return new BadRequestObjectResult(validateInputRes);
        }
        
        var meeting = mapper.InputToEntity(newInput);
        meeting.Members = input.MeetingMembers!.Select(x => new MeetingMember
        {
            UserId = x
        }).ToList();

        var overlapValidation = await meetingRepo.ValidateOverlapValidation(memberRepo: memberRepo, input: newInput, meeting: meeting, ct: ct);
        if (overlapValidation)
        {
            return new BadRequestObjectResult(validateInputRes);
        }

        await meetingRepo.AddAsync(meeting, ct);
        await meetingRepo.SaveChangesAsync(ct);
        return meeting;
    }

    public static async Task<ActionResult<Models.Meeting>> Update(this IMeetingRepository meetingRepo, IMeetingMemberRepository memberRepo, MeetingRouteDto route, MeetingInputDto input, CancellationToken ct = default)
    {
        var validateInputRes = input.ValidateInput();
        if (!validateInputRes)
        {
            return new BadRequestObjectResult(validateInputRes);
        }

        var meeting = await meetingRepo.FindAsync(route.MeetingId, ct);
        if (meeting is null)
        {
            return new NotFoundObjectResult(Messages.NotFount);
        }

        new MeetingMapper().InputToEntity(input, meeting);
        meeting.Members = input.MeetingMembers!.Select(x => new MeetingMember
        {
            UserId = x
        }).ToList();

        var overlapValidation = await meetingRepo.ValidateOverlapValidation(memberRepo: memberRepo, input: input, meeting: meeting, ct: ct);
        if (overlapValidation)
        {
            return new BadRequestObjectResult(overlapValidation);
        }

        await memberRepo.DeleteMembers(route.MeetingId);
        await meetingRepo.SaveChangesAsync(ct);

        return meeting;
    }

    public static async Task<ActionResult<Models.Meeting>> Cancel(this IMeetingRepository meetingRepo, MeetingRouteDto route, CancellationToken ct = default)
    {
        var meeting = await meetingRepo.FindAsync(route.MeetingId, ct);
        if (meeting is null)
        {
            return new NotFoundObjectResult(Messages.NotFount);
        }

        meeting.ModifiedDate = DateTimeOffset.UtcNow;
        meeting.Status = MeetingStatus.Canceled;

        await meetingRepo.SaveChangesAsync(ct);

        return meeting;
    }

    public static async Task<ActionResult<Models.Meeting>> SubmitResult(this IMeetingRepository meetingRepo, MeetingRouteDto route, MeetingResultInputDto input, CancellationToken ct = default)
    {
        var meeting = await meetingRepo.FindAsync(route.MeetingId, ct);
        if (meeting is null)
        {
            return new NotFoundObjectResult(Messages.NotFount);
        }

        meeting.ModifiedDate = DateTimeOffset.UtcNow;
        meeting.Result = input.Result;

        await meetingRepo.SaveChangesAsync(ct);

        return meeting;
    }

    private static ResponseDto<Models.Meeting> ValidateInput(this MeetingInputDto input)
    {
        if (input.MeetingMembers is null || input.MeetingMembers.Length <= 1)
        {
            return ResponseBase.Failed<Models.Meeting>(Messages.SelectMoreMember);
        }

        if (input.EndDateTime < input.StartDateTime)
        {
            return ResponseBase.Failed<Models.Meeting>(Messages.EndTimeShouldBiggerThanStart);
        }

        if (input.Type == MeetingType.InPerson && input.RoomId is null)
        {
            return ResponseBase.Failed<Models.Meeting>(Messages.RoomRequiredForInPersonMeeting);
        }

        if (input.Type == MeetingType.Online && string.IsNullOrEmpty(input.MeetingUrl))
        {
            return ResponseBase.Failed<Models.Meeting>(Messages.UrlRequiredForOnlineMeeting);
        }

        return ResponseBase.Success<Models.Meeting>();
    }

    private static async Task<ResponseDto<Models.Meeting>> ValidateOverlapValidation(this IMeetingRepository meetingRepo, MeetingInputDto input, IMeetingMemberRepository memberRepo, Models.Meeting meeting, CancellationToken ct = default)
    {
        if (await meetingRepo.HasOverLap(meeting: meeting))
        {
            return ResponseBase.Failed<Models.Meeting>(Messages.MeetingHasOverLap);
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

            return ResponseBase.Failed<Models.Meeting>(sbError.ToString());
        }

        return ResponseBase.Success<Models.Meeting>();
    }

    private static async Task<DateTimeOffset?> SuggestBestStartDateTime(this IMeetingRepository meetingRepo, MeetingIntelligenceInputDto input, CancellationToken ct = default)
    {
        var meetings = await meetingRepo.GetList(filter: new MeetingFilterDto
        {
            StatusArr = [MeetingStatus.Active],
            StartDateTime = DateTimeOffset.UtcNow,
            EndDateTime = DateTimeOffset.UtcNow.AddDays(7), // until next week from now
        }, ct: ct);

        if (meetings.Count == 0)
        {
            return DateTimeOffset.UtcNow;
        }
        
        foreach (var item in meetings)
        {
            var candidateStart  = item.EndDateTime;
            var candidateEnd  = item.EndDateTime.AddMinutes(input.ElapsedMinute);
            var overlaps = meetings.Any(x =>
                candidateStart < x.EndDateTime &&
                candidateEnd > x.StartDateTime
            );

            if (!overlaps)
            {
                return candidateStart;
            }
        }

        return null;
    }
}
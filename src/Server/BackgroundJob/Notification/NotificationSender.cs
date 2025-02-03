using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.BackgroundJob.Notification;

public class NotificationSender(IMeetingMemberRepository meetingMemberRepository)
{
    public async Task NotifyAheadMeetingMembers()
    {
        var meetingWillStartAt = DateTimeOffset.UtcNow.AddHours(2);
        
        var members = await meetingMemberRepository.GetByFilter(filter: new MeetingMemberFilterDto { MeetingStartDateTime = meetingWillStartAt });
        if (!members.Any())
        {
            return;
        }


        new NotificationBuilder(members).SendEmail().SendSms();
    }
}


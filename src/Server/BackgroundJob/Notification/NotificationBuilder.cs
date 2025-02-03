using Server.Modules.Meeting.Models;

namespace Server.BackgroundJob.Notification;

public class NotificationBuilder(IReadOnlyList<MeetingMember> meetingMembers)
{
    private IReadOnlyList<MeetingMember> _meetingMembers = meetingMembers;

    public NotificationBuilder SendEmail()
    {
        //todo: implement send email functionality
        return this;
    }
    
    public NotificationBuilder SendSms()
    {
        //todo: implement send email functionality
        return this;
    }
}
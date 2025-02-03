using Hangfire;
using Server.BackgroundJob.Notification;

namespace Server.BackgroundJob;

public static class HangfireHelper
{
    private const string NotifyAheadMeetingMembersJobId = "NotifyAheadMeetingMembers";
    
    public static void SetNotifyAheadMeetingMembersJobId()
    {
        RecurringJob.AddOrUpdate<NotificationSender>(
            NotifyAheadMeetingMembersJobId,
            job => job.NotifyAheadMeetingMembers(),
            Cron.Hourly);
    }
}
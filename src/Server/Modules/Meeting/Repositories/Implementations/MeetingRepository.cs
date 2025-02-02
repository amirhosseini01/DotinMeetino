using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.Meeting.Enums;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Implementations;

public class MeetingRepository(DataBaseContext context) : GenericRepository<Models.Meeting>(context), IMeetingRepository
{
    private readonly DbSet<Models.Meeting> _store = context.Meetings;

    public async Task<bool> HasOverLap(Models.Meeting meeting)
    {
        return await _store
            .Where(x => x.Status == MeetingStatus.Active)
            .AnyAsync(x =>
                    (meeting.StartDateTime >= x.StartDateTime && meeting.StartDateTime < x.EndDateTime) || // New meeting starts during an existing meeting
                    (meeting.StartDateTime > x.StartDateTime && meeting.StartDateTime <= x.EndDateTime) || // New meeting ends during an existing meeting
                    (meeting.StartDateTime <= x.StartDateTime && meeting.StartDateTime >= x.EndDateTime) // New meeting completely overlaps an existing meeting
            );
    }

    public async Task<bool> IsRoomAvailable(Models.Meeting meeting)
    {
        return meeting.Type == MeetingType.Online ||
               !await _store
                   .AnyAsync(x => x.RoomId == meeting.RoomId &&
                                  x.StartDateTime >= meeting.StartDateTime &&
                                  x.EndDateTime <= meeting.EndDateTime &&
                                  x.Type == MeetingType.InPerson &&
                                  x.Status == MeetingStatus.Active);
    }
}
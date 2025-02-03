using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Enums;
using Server.Modules.Meeting.Mapper;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Implementations;

public class MeetingRepository(DataBaseContext context) : GenericRepository<Models.Meeting>(context), IMeetingRepository
{
    private readonly DbSet<Models.Meeting> _store = context.Meetings;

    public async Task<bool> HasOverLap(Models.Meeting meeting)
    {
        var query = _store.Where(x => x.Status == MeetingStatus.Active);
        if (meeting is { Type: MeetingType.InPerson, RoomId: > 0 })
        {
            query = query.Where(x => x.RoomId == meeting.RoomId);
        }
        
        return await query.AnyAsync(x =>
                    (meeting.StartDateTime >= x.StartDateTime && meeting.StartDateTime < x.EndDateTime) || // New meeting starts during an existing meeting
                    (meeting.StartDateTime > x.StartDateTime && meeting.StartDateTime <= x.EndDateTime) || // New meeting ends during an existing meeting
                    (meeting.StartDateTime <= x.StartDateTime && meeting.StartDateTime >= x.EndDateTime) // New meeting completely overlaps an existing meeting
            );
    }


    public async Task<List<MeetingListDto>> GetList(MeetingFilterDto? filter = null, CancellationToken ct = default)
    {
        // todo: implement pagination
        var query = _store.AsNoTracking();
        if (filter is null)
        {
            return await query.SelectList().ToListAsync(ct); 
        }

        if (filter.StartDateTime is not null)
        {
            query = query.Where(x => x.StartDateTime >= filter.StartDateTime);
        }
        
        if (filter.EndDateTime is not null)
        {
            query = query.Where(x => x.EndDateTime <= filter.EndDateTime);
        }

        if (filter.StatusArr?.Length > 0)
        {
            query = query.Where(x => filter.StatusArr.Contains(x.Status));
        }
        
        return await query.SelectList().OrderBy(x=> x.StartDateTime).ToListAsync(ct);
    }
}
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Models;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Implementations;

public class MeetingMemberRepository(DataBaseContext context): GenericRepository<MeetingMember>(context), IMeetingMemberRepository
{
    private readonly DbSet<MeetingMember> _store = context.MeetingMembers;
    public async Task<IReadOnlyList<MeetingMember>> GetByFilter(MeetingMemberFilterDto filter, CancellationToken ct = default)
    {
        var query = _store.Include(x=> x.User).AsNoTracking();

        if (filter.MeetingStartDateTime is not null)
        {
            query = query.Where(x => x.Meeting!.StartDateTime >= filter.MeetingStartDateTime);
        }
        
        if (filter.MeetingEndDateTime is not null)
        {
            query = query.Where(x => x.Meeting!.EndDateTime <= filter.MeetingEndDateTime);
        }

        if (filter.UserIdArr?.Length > 0)
        {
            query = query.Where(x => filter.UserIdArr.Contains(x.UserId));
        }

        return await query.ToListAsync(ct);
    }
}
using Server.Data.Repositories.Contracts;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Models;

namespace Server.Modules.Meeting.Repositories.Contracts;

public interface IMeetingMemberRepository: IGenericRepository<MeetingMember> 
{
    Task<IReadOnlyList<MeetingMember>> GetByFilter(MeetingMemberFilterDto filter, CancellationToken ct = default);
    Task<int> DeleteMembers(int meetingId);
}
using Server.Data.Repositories.Contracts;
using Server.Modules.Meeting.Dto;

namespace Server.Modules.Meeting.Repositories.Contracts;

public interface IMeetingRepository: IGenericRepository<Models.Meeting>
{
    Task<bool> HasOverLap(Models.Meeting meeting);
    // todo: implement pagination
    Task<List<MeetingListDto>> GetList(CancellationToken ct = default);
}
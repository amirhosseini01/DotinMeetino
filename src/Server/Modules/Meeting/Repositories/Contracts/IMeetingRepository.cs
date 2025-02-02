using Server.Data.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Contracts;

public interface IMeetingRepository: IGenericRepository<Models.Meeting>
{
    Task<bool> HasOverLap(Models.Meeting meeting);
    Task<bool> IsRoomAvailable(Models.Meeting meeting);
}
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.Meeting.Repositories.Contracts;
using Server.Modules.User.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Implementations;

public class MeetingRepository(DataBaseContext context): GenericRepository<Models.Meeting>(context), IMeetingRepository
{
    
}
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.Meeting.Models;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Repositories.Implementations;

public class MeetingMemberRepository(DataBaseContext context): GenericRepository<MeetingMember>(context), IMeetingMemberRepository
{
    
}
using Server.Data;
using Server.Data.Repositories.Implementations;
using Server.Modules.User.Repositories.Contracts;

namespace Server.Modules.User.Repositories.Implementations;

public class UserRepository(DataBaseContext context): GenericRepository<Models.User>(context), IUserRepository
{
    
}
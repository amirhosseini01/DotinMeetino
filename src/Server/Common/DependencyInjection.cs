using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Modules.Meeting.Repositories.Contracts;
using Server.Modules.Meeting.Repositories.Implementations;
using Server.Modules.User.Repositories.Contracts;
using Server.Modules.User.Repositories.Implementations;

namespace Server.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddDataBase(this WebApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
    }

    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
        builder.Services.AddScoped<IMeetingMemberRepository, MeetingMemberRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
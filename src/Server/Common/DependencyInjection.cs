using Hangfire;
using Microsoft.EntityFrameworkCore;
using Server.BackgroundJob.Notification;
using Server.Data;
using Server.Modules.Meeting.Repositories.Contracts;
using Server.Modules.Meeting.Repositories.Implementations;

namespace Server.Common;

public static class DependencyInjection
{
    public const string SqlServerConnection = "SqlServer";
    public static IServiceCollection AddDataBase(this WebApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(SqlServerConnection)));
    }

    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        // todo: use "scrutor" to inject this type of dependency automatically
        builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
        builder.Services.AddScoped<IMeetingMemberRepository, MeetingMemberRepository>();
        builder.Services.AddScoped<NotificationSender>();
    }

    public static void AddHangfire(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString(SqlServerConnection)));
        
        builder.Services.AddHangfireServer();
    }
}
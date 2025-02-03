using Hangfire;
using Scalar.AspNetCore;
using Server.BackgroundJob;
using Server.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.AddDataBase();
builder.AddRepositories();
builder.AddHangfire();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // scalar/v1
    app.MapOpenApi();
    
    app.UseHangfireDashboard();
}

HangfireHelper.SetNotifyAheadMeetingMembersJobId();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
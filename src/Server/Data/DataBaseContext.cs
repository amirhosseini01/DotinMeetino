using Microsoft.EntityFrameworkCore;
using Server.Modules.Meeting.Models;
using Server.Modules.User.Models;

namespace Server.Data;

public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingMember> MeetingMembers { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Room>()
            .Property(m => m.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<User>()
            .Property(m => m.Id)
            .ValueGeneratedNever();
    }
}

/*
  dotnet ef migrations add Init --project src/Server
  dotnet ef database update --project src/Server
  dotnet ef migrations remove --project src/Server
 */
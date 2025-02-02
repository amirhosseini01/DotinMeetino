using Microsoft.EntityFrameworkCore;
using Server.Modules.Meeting.Models;
using Server.Modules.User.Models;

namespace Server.Data;

public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingMember> MeetingMembers { get; set; }
    public DbSet<User> Users { get; set; }
}
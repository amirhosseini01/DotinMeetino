using Server.Data.Models;

namespace Server.Modules.Meeting.Models;

public class MeetingMember: BaseEntity
{
    public int MeetingId { get; set; }
    public required int UserId { get; set; }

    public Meeting? Meeting { get; set; }
    public User.Models.User? User { get; set; }
}
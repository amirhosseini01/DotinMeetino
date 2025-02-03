using Riok.Mapperly.Abstractions;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Models;

namespace Server.Modules.Meeting.Mapper;

[Mapper]
public partial class MeetingMapper
{
    public partial Models.Meeting InputToEntity(MeetingInputDto input);
    public partial void InputToEntity(MeetingInputDto input, Models.Meeting entity);
}

public static class MeetingMapperQuery
{
    public static IQueryable<MeetingListDto> SelectList(this IQueryable<Models.Meeting> query)
    {
        return query.Select(x => new MeetingListDto
        {
            MemberCount = x.Members!.Count(),
            Title = x.Title,
            EndDateTime = x.EndDateTime,
            Status = x.Status,
            StartDateTime = x.StartDateTime,
            Type = x.Type,
            RoomId = x.RoomId,
            Room = new Room
            {
                Title = x.Room!.Title
            },
            Result = x.Result,
            MeetingUrl = x.MeetingUrl,
            CreatedDate = x.CreatedDate,
            ModifiedDate = x.ModifiedDate,
            Id = x.Id
        });
    }
}
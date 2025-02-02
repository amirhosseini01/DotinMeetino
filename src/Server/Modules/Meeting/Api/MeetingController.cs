using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Modules.Meeting.Repositories.Contracts;

namespace Server.Modules.Meeting.Api;

//todo: use authentication and authorization attribute
[ApiController]
[Route("api/[controller]")]
public class MeetingController(IMeetingRepository meetingRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(Models.Meeting meeting)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (await meetingRepository.HasOverLap(meeting: meeting))
        {
            return BadRequest(Messages.MeetingHasOverLap);
        }
        
        if (!await meetingRepository.IsRoomAvailable(meeting: meeting))
        {
            return BadRequest(Messages.RoomIsNotAvailableForMeeting);
        }
        
        await meetingRepository.AddAsync(meeting);
        await meetingRepository.SaveChangesAsync();
        return Ok(Messages.MeetingCreatedSuccessfully);
    }
}
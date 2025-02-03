using Microsoft.AspNetCore.Mvc;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Repositories.Contracts;
using Server.Modules.Meeting.Services;

namespace Server.Modules.Meeting.Api;

//todo: use authentication and authorization attribute
[ApiController]
[Route("api/[controller]")]
public class MeetingController(IMeetingRepository meetingRepo,
    IMeetingMemberRepository memberRepo) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Models.Meeting>> Post(MeetingInputDto input, CancellationToken ct= default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Create(memberRepo: memberRepo, input: input, ct: ct);
    }
}
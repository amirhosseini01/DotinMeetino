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
    
    [HttpGet]
    public async Task<ActionResult<Models.Meeting>> Cancel([FromRoute] MeetingRouteDto route, CancellationToken ct= default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Cancel(route: route, ct: ct);
    }
    
    [HttpPost]
    public async Task<ActionResult<Models.Meeting>> SubmitResult([FromRoute] MeetingRouteDto route, MeetingResultInputDto input, CancellationToken ct= default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.SubmitResult(route: route, input: input, ct: ct);
    }
}
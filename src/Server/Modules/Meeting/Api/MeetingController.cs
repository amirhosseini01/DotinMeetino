using Microsoft.AspNetCore.Mvc;
using Server.Modules.Meeting.Dto;
using Server.Modules.Meeting.Repositories.Contracts;
using Server.Modules.Meeting.Services;

namespace Server.Modules.Meeting.Api;

//todo: use authentication and authorization attribute
[ApiController]
[Route("api/[controller]")]
public class MeetingController(
    IMeetingRepository meetingRepo,
    IMeetingMemberRepository memberRepo) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Models.Meeting>> Post(MeetingInputDto input, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Create(memberRepo: memberRepo, input: input, ct: ct);
    }

    [HttpPut(nameof(Cancel))]
    public async Task<ActionResult<Models.Meeting>> Cancel([FromQuery] MeetingRouteDto route, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Cancel(route: route, ct: ct);
    }

    [HttpPut(nameof(SubmitResult))]
    public async Task<ActionResult<Models.Meeting>> SubmitResult([FromQuery] MeetingRouteDto route, MeetingResultInputDto input, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.SubmitResult(route: route, input: input, ct: ct);
    }

    [HttpPut]
    public async Task<ActionResult<Models.Meeting>> Put([FromQuery] MeetingRouteDto route, MeetingInputDto input, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Update(memberRepo: memberRepo, route: route, input: input, ct: ct);
    }

    [HttpGet]
    public async Task<ActionResult<List<MeetingListDto>>> Get()
    {
        return await meetingRepo.GetList();
    }

    [HttpPost]
    public async Task<ActionResult<Models.Meeting>> IntelligenceAdd(MeetingIntelligenceInputDto input, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return await meetingRepo.Create(memberRepo: memberRepo, input: input, ct: ct);
    }
}
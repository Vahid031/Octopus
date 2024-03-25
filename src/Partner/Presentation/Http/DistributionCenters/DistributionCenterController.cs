using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octopus.Presentation.Http.EnvelopModels;

namespace Octopus.Partner.Presentation.Http.DistributionCenters;

[ApiController]
[Route("api/partner/distribution-centers")]
[Produces("application/json")]
[Consumes("application/json")]
public class DistributionCenterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DistributionCenterController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(SuccessEnvelop<UserAssignedDistributionCenterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpGet("_my")]
    public async Task<ActionResult<UserAssignedDistributionCenterResponse>> GetUserAssigenedDistributionCenters()
    {
        //var command = new SignInWithPasswordCommand
        //{
        //    IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
        //    Password = request.Password,
        //    UserName = request.UserName,
        //};
        //var result = await _mediator.Send(command);

        return _mapper.Map<UserAssignedDistributionCenterResponse>("");
    }
}


public record UserAssignedDistributionCenterResponse(Guid Id, string Name, string ImageUrl, List<UserAssignedDistributionCenterBrandModel> Brands);
public record UserAssignedDistributionCenterBrandModel(Guid Id, string Name, string ImageUrl);


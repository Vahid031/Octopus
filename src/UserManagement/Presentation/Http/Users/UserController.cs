using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octopus.Presentation.Http;
using Octopus.Presentation.Http.EnvelopModels;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;
using Octopus.UserManagement.Core.Contract.Users.Commands.SetPasswordWithOtp;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Presentation.Http.Users.Models;

namespace Octopus.UserManagement.Presentation.Http.Users;

[ApiController]
[Route("api/user-management/users")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(SuccessEnvelop<SignInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [HttpPost("password/sign-in")]
    public async Task<ActionResult<SignInResponse>> SignInWithPassword([FromBody] SignInWithPasswordRequest request)
    {
        var command = new SignInWithPasswordCommand
        {
            IpAddress = "127.0.0.1",
            Password = request.Password,
            UserName = request.UserName,
        };
        var result = await _mediator.Send(command);

        return _mapper.Map<SignInResponse>(result);
    }

    [ProducesResponseType(typeof(SuccessEnvelop<SignInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [HttpPost("otp/sign-in")]
    public async Task<ActionResult<SignInResponse>> SignInWithOtp([FromBody] SignInWithOtpRequest request)
    {
        var command = new SignInWithOtpCommand
        {
            IpAddress = "127.0.0.1",
            Code = request.Code,
            UserName = request.UserName,
        };
        var result = await _mediator.Send(command);

        return _mapper.Map<SignInResponse>(result);
    }

    [ProducesResponseType(typeof(SuccessEnvelop), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPut("password/change")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand
        {
            OldPassword = request.OldPassword,
            NewPassword = request.NewPassword,
            UserId = HttpContext.GetUserId()
        };
        await _mediator.Send(command);

        return NoContent();
    }

    [ProducesResponseType(typeof(SuccessEnvelop), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPut("otp/set-password")]
    public async Task<ActionResult> SetPassword([FromBody] SetPasswordWithOtpRequest request)
    {
        var command = new SetPasswordWithOtpCommand
        {
            Code = request.Code,
            NewPassword = request.NewPassword,
            ComparePassword = request.ComparePassword,
            UserId = HttpContext.GetUserId()
        };

        await _mediator.Send(command);

        return NoContent();
    }

    [ProducesResponseType(typeof(SuccessEnvelop), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [HttpPost("otp/send")]
    public async Task<ActionResult> SendOtp([FromBody] SendOtpRequest request)
    {
        var command = new SendOtpCommand()
        {
            UserName = request.UserName,
            IpAddress = "127.0.0.1"
        };
        await _mediator.Send(command);

        return NoContent();
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignOut;
using Octopus.UserManagement.Presentation.Http.Users.Models;

namespace Octopus.UserManagement.Presentation.Http.Users;

[ApiController]
[Route("api/catalog/users")]
public class UserController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignInWithPassword([FromBody] SignInWithPasswordRequest request)
    {
        var command = new SignInWithPasswordCommand()
        {
            IpAddress = "127.0.0.1",
            Password = request.Password,
            UserName = request.UserName,
        };
        var result = await mediator.Send(command);

        return mapper.Map<SignInResponse>(result);
    }

    [HttpPost("signout")]
    public new async Task<ActionResult> SignOut()
    {
        var command = new SignOutCommand
        {
            UserId = ""
        };
        await mediator.Send(command);

        return NoContent();
    }
}

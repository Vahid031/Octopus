using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignOut;
using Octopus.UserManagement.Presentation.Http.Users.Models;
using System.Security.Claims;

namespace Octopus.UserManagement.Presentation.Http.Users;

[ApiController]
[Route("api/catalog/users")]
public class UserController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignInWithPassword([FromBody] SignInWithPasswordRequest request)
    {
        //var command = new SignInWithPasswordCommand()
        //{
        //    IpAddress = "127.0.0.1",
        //    Password = request.Password,
        //    UserName = request.UserName,
        //};
        //var result = await mediator.Send(command);

        var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "Vahid", ClaimValueTypes.String)
            };
        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "", "");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(userIdentity),
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(10),
                IsPersistent = true,
                AllowRefresh = true
            });

        return NoContent();

        //return mapper.Map<SignInResponse>(result);
    }

    [HttpPost("signout")]
    public new async Task<ActionResult> SignOut()
    {
        //var command = new SignOutCommand
        //{
        //    UserId = ""
        //};
        //await mediator.Send(command);

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


        return NoContent();
    }
}

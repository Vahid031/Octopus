﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octopus.Presentation.Http;
using Octopus.Presentation.Http.EnvelopModels;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.RefreshToken;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;
using Octopus.UserManagement.Core.Contract.Users.Commands.SetPassword;
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
			IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
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
			IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
			Code = request.Code,
			PhoneNumber = request.PhoneNumber,
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
	[HttpPut("password/set")]
	public async Task<ActionResult> SetPassword([FromBody] SetPasswordRequest request)
	{
		var command = new SetPasswordCommand
		{
			NewPassword = request.NewPassword,
			UserId = HttpContext.GetUserId()
		};

		await _mediator.Send(command);

		return NoContent();
	}

	[ProducesResponseType(typeof(SuccessEnvelop<SendOtpResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
	[HttpPost("otp/send")]
	public async Task<ActionResult<SendOtpResponse>> SendOtp([FromBody] SendOtpRequest request)
	{
		var command = new SendOtpCommand()
		{
			PhoneNumber = request.PhoneNumber,
			IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
        };

		var expires = await _mediator.Send(command);

		return new SendOtpResponse() { Expires = expires };
	}

    [ProducesResponseType(typeof(SuccessEnvelop<SendOtpResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<SignInResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand()
        {
            UserName = request.UserName,
			Token = request.Token,
            IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
        };

        var result = await _mediator.Send(command);

        return _mapper.Map<SignInResponse>(result); ;
    }

    [ProducesResponseType(typeof(SuccessEnvelop), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(EnvelopError), StatusCodes.Status400BadRequest)]
	[HttpPost("__register")]
	public async Task<ActionResult> Register([FromBody] RegisterCommand request)
	{

		await _mediator.Send(request);

		return NoContent();
	}
}

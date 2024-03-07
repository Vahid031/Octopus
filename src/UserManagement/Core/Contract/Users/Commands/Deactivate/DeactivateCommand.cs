﻿using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.Deactivate;

public class DeactivateCommand : IRequest
{
	public UserId UserId { get; init; }
}
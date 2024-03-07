﻿using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserPasswordMustBeAtLeast3CharacterException : OctopusDomainException
{
	public UserPasswordMustBeAtLeast3CharacterException(string password) : base("User password: '{password}' must be at least 3 character")
	{
	}
}
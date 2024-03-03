﻿using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Models;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IUserTokenGenerator
{
    TokenModel GenerateToken(User user);
}
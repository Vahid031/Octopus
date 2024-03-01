﻿namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInWithPasswordRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}
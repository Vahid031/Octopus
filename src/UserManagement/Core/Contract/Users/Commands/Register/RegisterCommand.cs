using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.Register;

public record RegisterCommand : IRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string Username { get; init; }
}
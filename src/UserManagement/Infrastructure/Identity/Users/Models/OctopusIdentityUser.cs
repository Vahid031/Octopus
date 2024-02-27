using Microsoft.AspNetCore.Identity;

namespace Octopus.UserManagement.Core.Identity.Users.Models;

public class OctopusIdentityUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
using System.Diagnostics;
using System.Security.Claims;
using Octopus.Core.Contract.Exceptions;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Presentation.Http;

public static class ClaimExtensions
{
	//public static Guid GetUserId(this ClaimsPrincipal principal)
	//{
	//	var userIdClaim = principal.FindFirstValue(ClaimTypes.NameIdentifier);
	//	if (string.IsNullOrWhiteSpace(userIdClaim))
	//		throw new OctopusException("User not found");
	//	var userId = Guid.Parse(userIdClaim);
	//	if (Activity.Current != null)
	//		Activity.Current.AddTag("user.id", userId.ToString());
	//	return userId;
	//}

	public static UserId GetUserId(this HttpContext httpContext)
	{
		if (httpContext.User.Identity is not { IsAuthenticated: true })
			throw new OctopusException("User not found");

		var userIdClaim = httpContext.User.FindFirstValue("uid");
		if (userIdClaim == null)
            throw new OctopusException("User not found");

        if (!Guid.TryParse(userIdClaim, out var userId))
            throw new OctopusException("User not found");

        if (Activity.Current != null)
			Activity.Current.AddTag("user.id", userId);

		return UserId.Create(userId);
	}

	public static UserId GetRequiredUserId(this HttpContext httpContext)
	{
		var userId = httpContext.GetUserId();

		if (userId == null)
			throw new OctopusException("user not authorized");

		return userId;
	}

}

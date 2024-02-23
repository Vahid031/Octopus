//using System.Diagnostics;
//using System.Security.Claims;

//namespace Squidward.Presentation.Adapter.Http;

//public static class ClaimExtensions
//{
//	public static Guid GetUserId(this ClaimsPrincipal principal)
//	{
//		var userIdClaim = principal.FindFirstValue(ClaimTypes.NameIdentifier);
//		if (string.IsNullOrWhiteSpace(userIdClaim))
//			throw new SquidwardNotAuthorizedException("User not found");
//		var userId = Guid.Parse(userIdClaim);
//		if (Activity.Current != null)
//			Activity.Current.AddTag("user.id", userId.ToString());
//		return userId;
//	}

//	public static UserId? GetUserId(this HttpContext httpContext)
//	{
//		if (httpContext.User.Identity is not { IsAuthenticated: true })
//			return null;

//		var userIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//		if (userIdClaim == null)
//			return null;

//		if (!Guid.TryParse(userIdClaim, out var userId))
//			return null;

//		if (Activity.Current != null)
//			Activity.Current.AddTag("user.id", userId);

//		return UserId.Create(userId);
//	}

//	public static UserId GetRequiredUserId(this HttpContext httpContext)
//	{
//		var userId = httpContext.GetUserId();

//		if (userId == null)
//			throw new SquidwardNotAuthorizedException("user not authorized");

//		return userId;
//	}

//}

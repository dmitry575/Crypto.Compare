using System.Security.Claims;
using Crypto.Compare.PublicApi.Constants;

namespace Crypto.Compare.PublicApi.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Get real ip address client
    /// </summary>
    /// <param name="context">HTTP context</param>
    /// <param name="mapToIPv4">Mapping ip address to IPv4</param>
    public static string GetRealIp(this HttpContext context, bool mapToIPv4 = true)
    {
        if (context == null)
        {
            return string.Empty;
        }

        return context.Request?.Headers["X-Forwarded-For"].FirstOrDefault() ?? (mapToIPv4
            ? context.Connection?.RemoteIpAddress?.MapToIPv4().ToString()
            : context.Connection?.RemoteIpAddress?.ToString());
    }

    public static Guid RequestId(this HttpContext httpContext)
    {
        return Guid.TryParse(httpContext?.Request?.Headers[LogConstants.XRequestId].ToString(), out var requestId)
            ? requestId
            : Guid.Empty;
    }

    public static long UserId(this HttpContext httpContext)
    {
        return long.TryParse(httpContext.GetClaimValue("user_id"), out var result) ? result : 0;
    }

    private static string? GetClaimValue(this HttpContext httpContext, string name)
    {
        return httpContext
            .GetClaims()
            .Where(x => x.Type == name)
            .Select(x => x.Value)
            .FirstOrDefault();
    }

    private static IEnumerable<Claim> GetClaims(this HttpContext httpContext)
    {
        return httpContext.User?.Identity is not ClaimsIdentity identity ? Enumerable.Empty<Claim>() : identity.Claims;
    }
}

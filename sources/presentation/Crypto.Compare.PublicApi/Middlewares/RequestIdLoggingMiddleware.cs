using Crypto.Compare.PublicApi.Constants;
using Microsoft.Extensions.Primitives;

namespace Crypto.Compare.PublicApi.Middlewares;

/// <summary>
/// Set request id to all requests
/// </summary>
public class RequestIdLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestIdLoggingMiddleware> _logger;

    public RequestIdLoggingMiddleware(RequestDelegate next, ILogger<RequestIdLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        string str = context.Request.Headers[LogConstants.XRequestId].ToString();
        if (string.IsNullOrEmpty(str))
        {
            str = Guid.NewGuid().ToString();
        }

        using (_logger.BeginScope(str))
        {
            if (context.Response != null && context.Response.Headers != null &&
                !context.Response.Headers.ContainsKey(LogConstants.XRequestId))
            {
                context.Response.Headers.Add(new KeyValuePair<string, StringValues>(LogConstants.XRequestId, (StringValues)str));
            }

            await _next(context);
        }
    }
}

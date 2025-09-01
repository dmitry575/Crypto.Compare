using Crypto.Compare.PublicApi.Constants;
using Crypto.Compare.PublicApi.Extensions;
using log4net;

namespace Crypto.Compare.PublicApi.Middlewares;

public sealed class UserIdLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdLoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.UserId();
        if (!userId.HasValue) return _next(context);

        using (LogicalThreadContext.Stacks[LogConstants.NDCPropertyName].Push(userId.ToString()))
        {
            return _next(context);
        }
    }
}
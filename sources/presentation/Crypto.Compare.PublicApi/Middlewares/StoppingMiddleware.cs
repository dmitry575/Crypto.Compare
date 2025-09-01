namespace Crypto.Compare.PublicApi.Middlewares;

public class StoppingMiddleware
{
    private const string Path = "/healthcheck";

    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly ILogger<StoppingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public StoppingMiddleware(RequestDelegate next,
        IHostApplicationLifetime applicationLifetime,
        ILogger<StoppingMiddleware> logger)
    {
        _applicationLifetime = applicationLifetime;
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // if come the signal stop application
        if (IsApplicationStopping(context.Request.Path.Value))
        {
            _logger.LogWarning($"Applications is stopping, request not serviced {context.Request.Path}");
            context.Response.StatusCode = 503;
            return;
        }

        await _next.Invoke(context);
    }

    /// <summary>
    ///     Проверка, что приложение останавливается
    /// </summary>
    private bool IsApplicationStopping(string path)
    {
        return !string.IsNullOrEmpty(path)
               && path.Equals(Path, StringComparison.InvariantCultureIgnoreCase)
               && (_applicationLifetime?.ApplicationStopping.IsCancellationRequested ?? false);
    }
}
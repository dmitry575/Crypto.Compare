namespace Crypto.Compare.PublicApi.Middlewares;

/// <summary>
/// Listen events for abort
/// </summary>
public class AbortRequestMiddleware
{
    private readonly ILogger<AbortRequestMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public AbortRequestMiddleware(RequestDelegate next, 
        IHostApplicationLifetime applicationLifetime,
        ILogger<AbortRequestMiddleware> logger)
    {
        _applicationLifetime = applicationLifetime;

        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        using (_applicationLifetime.ApplicationStopping.Register(StoppingRequest, context))
        using (_applicationLifetime.ApplicationStopped.Register(AbortRequest, context))
        {
            await _next.Invoke(context);
        }
    }
    
    /// <summary>
    /// Aborted request and logged information into log
    /// </summary>
    private void AbortRequest(object? context)
    {
        var httpContext = context as HttpContext;
        _logger.LogInformation($"Aborting request: {httpContext?.Request.Path} because application is shutdown");
        try
        {
            httpContext?.Abort();
        }
        catch (Exception e)
        {
            _logger.LogError($"Abort request failed: {httpContext?.Request.Path}", e);
        }
    }

    /// <summary>
    /// Get first signal
    /// </summary>
    private void StoppingRequest(object? context)
    {
        var httpContext = context as HttpContext;
        _logger.LogInformation($"Stopping request: {httpContext?.Request.Path} because application is shutting down");
    }
}

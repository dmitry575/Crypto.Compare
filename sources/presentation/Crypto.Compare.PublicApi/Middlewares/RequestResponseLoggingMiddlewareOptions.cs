namespace Crypto.Compare.PublicApi.Middlewares;

public class RequestResponseLoggingMiddlewareOptions
{
    /// <summary>
    ///     Turn on logging requests
    /// </summary>
    public bool EnableRequestLogging { get; set; }

    /// <summary>
    ///     Turn on logging response
    /// </summary>
    public bool EnableResponseLogging { get; set; }
}
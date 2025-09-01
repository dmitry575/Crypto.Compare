using System.Net;
using Crypto.Compare.Common.Common.Errors;
using Crypto.Compare.PublicApi.Responses;
using Newtonsoft.Json;

namespace Crypto.Compare.PublicApi.Middlewares;

/// <summary>
///     Try handle exception from any controller
/// </summary>
public class UnhandledExceptionMiddleware
{
    private readonly ILogger<UnhandledExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    ///     .Constr
    /// </summary>
    /// <param name="next">Next handler of request</param>
    /// <param name="logger">logger where write log</param>
    public UnhandledExceptionMiddleware(
        RequestDelegate next,
        ILogger<UnhandledExceptionMiddleware> logger
    )
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                $"Unhandled exception when calling '{context.Request.Path}', headers sent: {context.Response.HasStarted}");

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(GetSystemErrorResponseBody());
            }
        }
    }

    private string GetSystemErrorResponseBody()
    {
        var errorMessage = "System error. Please try again later";

        var response = new BaseApiResponse();
        response.AddErrorMsg((int)SystemErrorCodes.SystemError, errorMessage);

        return JsonConvert.SerializeObject(response);
    }
}
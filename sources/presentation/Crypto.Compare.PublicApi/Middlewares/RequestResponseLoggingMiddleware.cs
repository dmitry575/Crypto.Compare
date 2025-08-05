using Crypto.Compare.PublicApi.Constants;
using log4net;
using Microsoft.IO;

namespace Crypto.Compare.PublicApi.Middlewares;

/// <summary>
/// Logging of requests
/// https://github.com/elanderson/ASP.NET-Core-Basics-Refresh/blob/master/src/ContactsApi/Middleware/RequestResponseLoggingMiddleware.cs
/// </summary>
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    private readonly RequestResponseLoggingMiddlewareOptions _options;

    public RequestResponseLoggingMiddleware(RequestDelegate next,
        ILogger<RequestResponseLoggingMiddleware> logger,
        RequestResponseLoggingMiddlewareOptions options)
    {
        _next = next;
        _logger = logger;
        _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        _options = options;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_options.EnableRequestLogging)
        {
            await LogRequest(context);
        }

        if (_options.EnableResponseLogging)
        {
            await LogResponseAsync(context);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        await using var requestStream = _recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);

        string requestId = context.Request.Headers[LogConstants.XRequestId].ToString();
        using (LogicalThreadContext.Stacks[LogConstants.NDCPropertyName].Push(requestId))
        {
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }
    }

    private async Task LogResponseAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        await using var responseBody = _recyclableMemoryStreamManager.GetStream();
        context.Response.Body = responseBody;

        await _next(context);

        var text = string.Empty;
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using (var stream = new StreamReader(context.Response.Body))
        {
            text = await stream.ReadToEndAsync();
        }

        context.Response.Body.Seek(0, SeekOrigin.Begin);

        string requestId = context.Request.Headers[LogConstants.XRequestId].ToString();
        using (LogicalThreadContext.Stacks[LogConstants.NDCPropertyName].Push(requestId))
        {
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");
        }

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);

        return textWriter.ToString();
    }
}

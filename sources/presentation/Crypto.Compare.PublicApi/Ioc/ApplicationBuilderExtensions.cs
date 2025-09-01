namespace Crypto.Compare.PublicApi.Ioc;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     base url for application
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private const string BaseServerUrl = "ASPNETCORE_BASE_SERVER_URL";

    /// <summary>
    ///     Set base url
    /// </summary>
    public static IApplicationBuilder UsePathBaseFromEnvironmentVariables(this IApplicationBuilder app)
    {
        var basePathUrl = Environment.GetEnvironmentVariable(BaseServerUrl);
        if (!string.IsNullOrWhiteSpace(basePathUrl)) app.UsePathBase(basePathUrl);

        return app;
    }
}
using System.Diagnostics.CodeAnalysis;
using Crypto.Compare.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Compare.PublicApi.Ioc;

[ExcludeFromCodeCoverage]
public static class DatabaseRegistry
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        services.AddDbContext<CryptoCompareContext>(options => options.UseNpgsql(connectionString));

//        services.AddScoped(provider => new ContextFactory<CryptoCompareContext>(() =>
//            ActivatorUtilities.CreateInstance<CryptoCompareContext>(provider,
//provider.GetRequiredService<DbContextOptions<CryptoCompareContext>>())));

        return services;
    }
}

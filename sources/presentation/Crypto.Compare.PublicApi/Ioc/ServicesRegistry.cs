using System.Reflection;
using AutoMapper;
using Crypto.Compare.PublicApi.Mapping;
using Crypto.Compare.Services.Configs;

namespace Crypto.Compare.PublicApi.Ioc;

public static class ServicesRegistry
{
    internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                .AddConfigurations(configuration)
                .AddMediator()
                .AddSingletons()
                .AddExternalServices()
            ;
    }

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<AppSettingsConfig>(configuration.GetSection("AppSettings").Bind);

    private static IServiceCollection AddSingletons(this IServiceCollection services)
        => services
            .AddSingleton<IResponseMapper, ResponseMapper>()
            .AddSingleton(_ => new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); }).CreateMapper());


    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
        => services
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetCallingAssembly()));
}
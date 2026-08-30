using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Bunny.HttpClients.Registrars;
using Soenneker.Bunny.OpenApiClientUtil.Abstract;

namespace Soenneker.Bunny.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class BunnyOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IBunnyOpenApiClientUtil"/> as a singleton service backed by the singleton HTTP-client provider.
    /// </summary>
    public static IServiceCollection AddBunnyOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddBunnyOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IBunnyOpenApiClientUtil, BunnyOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IBunnyOpenApiClientUtil"/> as a scoped service backed by the singleton HTTP-client provider.
    /// </summary>
    public static IServiceCollection AddBunnyOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddBunnyOpenApiHttpClientAsSingleton()
                .TryAddScoped<IBunnyOpenApiClientUtil, BunnyOpenApiClientUtil>();

        return services;
    }
}

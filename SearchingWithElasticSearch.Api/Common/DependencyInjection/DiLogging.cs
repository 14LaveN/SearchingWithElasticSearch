using Serilog;

namespace SearchingWithElasticSearch.Api.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for logging.
/// </summary>
internal static class DiLogging
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddLoggingExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        
        return services;
    }
}
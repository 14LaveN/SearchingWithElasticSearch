using SearchingWithElasticSearch.Database;

namespace SearchingWithElasticSearch.Api.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for database.
/// </summary>
internal static class DiDatabase
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddElasticDatabase();
        
        return services;
    }
}
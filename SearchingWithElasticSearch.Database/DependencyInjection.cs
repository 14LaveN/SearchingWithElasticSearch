using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchingWithElasticSearch.Application.Core.Settings;
using SearchingWithElasticSearch.Database.Data.Interfaces;
using SearchingWithElasticSearch.Database.Data.Repository;

namespace SearchingWithElasticSearch.Database;

/// <summary>
/// Represents elastic database dependency injection container class.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddElasticDatabase(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<ElasticSettings>(_ => new ElasticSettings());
        
        services.AddScoped<ISearchRepository, SearchRepository>();
        
        services.AddHealthChecks()
            .AddElasticsearch(ElasticSettings.ConnectionString);

        return services;
    }
}
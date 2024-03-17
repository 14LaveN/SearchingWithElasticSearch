using MediatR;
using MediatR.NotificationPublishers;
using SearchingWithElasticSearch.Api.Mediatr.Queries.SearchDocuments;
using SearchingWithElasticSearch.Application.Core.Behaviours;

namespace SearchingWithElasticSearch.Api.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for <see cref="MediatR"/>.
/// </summary>
internal static class DiMediator
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<Program>();

            x.RegisterServicesFromAssemblies(typeof(SearchDocumentsQuery).Assembly,
                typeof(SearchDocumentsQueryHandler).Assembly);
            
            x.NotificationPublisher = new TaskWhenAllPublisher();
            x.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            
            x.Lifetime = ServiceLifetime.Transient;
            
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(MetricsBehaviour<,>));
            x.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        
        return services;
    }
}
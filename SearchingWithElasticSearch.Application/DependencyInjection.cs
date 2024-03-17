using SearchingWithElasticSearch.Application.ApiHelpers.ExceptionHandler;
using Microsoft.Extensions.DependencyInjection;
using SearchingWithElasticSearch.Application.Common;
using SearchingWithElasticSearch.Application.Core.Abstractions.Common;
using SearchingWithElasticSearch.Application.Core.Helpers.Metric;

namespace SearchingWithElasticSearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentException();
        
        services.AddScoped<CreateMetricsHelper>();
        services.AddScoped<IDateTime, MachineDateTime>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }
}
using FluentValidation;

namespace SearchingWithElasticSearch.Api.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for validators.
/// </summary>
internal static class DiValidators
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        //services.AddScoped<IValidator<CreateHistogramCommand>, CreateHistogramCommandValidator>();
        
        return services;
    }
}
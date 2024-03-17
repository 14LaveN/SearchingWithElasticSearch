using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SearchingWithElasticSearch.BackgroundTasks.QuartZ;
using SearchingWithElasticSearch.BackgroundTasks.QuartZ.Jobs;
using SearchingWithElasticSearch.BackgroundTasks.QuartZ.Schedulers;

namespace SearchingWithElasticSearch.BackgroundTasks;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddBackgroundTasks(
        this IServiceCollection services)
    {
        services.AddMediatR(x=>
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(IndexDocumentsJob));

            configure
                .AddJob<IndexDocumentsJob>(jobKey);
            
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        
        services.AddTransient<IJobFactory, QuartzJobFactory>();
        services.AddSingleton(_ =>
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            
            return scheduler;
        });
        
        services.AddTransient<IndexDocumentsScheduler>();
        
        var scheduler = new IndexDocumentsScheduler();
        scheduler.Start(services);
        
        return services;
    }
}
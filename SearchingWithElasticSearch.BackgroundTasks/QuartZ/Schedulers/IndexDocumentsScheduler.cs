using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SearchingWithElasticSearch.BackgroundTasks.QuartZ.Jobs;

namespace SearchingWithElasticSearch.BackgroundTasks.QuartZ.Schedulers;

/// <summary>
/// Represents the save metrics scheduler class.
/// </summary>
public sealed class IndexDocumentsScheduler
    : AbstractScheduler<IndexDocumentsJob>
{
    /// <summary>
    /// Starts the job.
    /// </summary>
    public override async void Start(IServiceCollection serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            //scheduler.JobFactory = serviceProvider.GetService<QuartzJobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<IndexDocumentsJob>().Build();
        ITrigger trigger = TriggerBuilder
            .Create()
            .WithIdentity($"{nameof(IndexDocumentsJob)}Trigger", "default")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(300)
                .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}
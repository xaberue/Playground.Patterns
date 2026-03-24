using Hangfire;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;

public class HangfireRenewJobScheduler : IHostedService
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly ILogger<HangfireRenewJobScheduler> _logger;


    public HangfireRenewJobScheduler(IRecurringJobManager recurringJobManager, ILogger<HangfireRenewJobScheduler> logger)
    {
        _recurringJobManager = recurringJobManager;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering recurring Hangfire jobs...");

        _recurringJobManager.AddOrUpdate<RenewJob>(
            "renew-job",
            job => job.ExecuteAsync(CancellationToken.None),
            Cron.MinuteInterval(1)
            //Cron.Daily(1)
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

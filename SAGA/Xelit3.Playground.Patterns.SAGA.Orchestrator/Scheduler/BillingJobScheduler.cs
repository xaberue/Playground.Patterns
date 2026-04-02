using Hangfire;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;

public class BillingJobScheduler : IHostedService
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly ILogger<BillingJobScheduler> _logger;


    public BillingJobScheduler(IRecurringJobManager recurringJobManager, ILogger<BillingJobScheduler> logger)
    {
        _recurringJobManager = recurringJobManager;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering recurring Hangfire jobs...");

        _recurringJobManager.AddOrUpdate<BillingJob>(
            "billing-job",
            job => job.ExecuteAsync(CancellationToken.None),
            //Cron.MinuteInterval(1)
            Cron.Daily(1)
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

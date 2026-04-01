using Microsoft.EntityFrameworkCore;
using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;

public class BillingJob
{

    private readonly ILogger<BillingJob> _logger;
    private readonly IMessageBus _bus;
    private readonly BillingDbContext _billingDbContext;


    public BillingJob(ILogger<BillingJob> logger, IMessageBus bus, BillingDbContext billingDbContext)
    {
        _logger = logger;
        _bus = bus;
        _billingDbContext = billingDbContext;
    }


    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var processEntity = new BillingProcess();
        await _billingDbContext.BillingProcesses.AddAsync(processEntity);

        var request = new BillingJobExecutionRequestEvent(Guid.NewGuid(), DateTime.Now.Day);
        
        await _bus.SendAsync(request);
        await _billingDbContext.SaveChangesAsync();
    }
}
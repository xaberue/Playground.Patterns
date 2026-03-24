using Wolverine;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;

public class RenewJob
{

    private readonly ILogger<RenewJob> _logger;
    private readonly IMessageBus _bus;


    public RenewJob(ILogger<RenewJob> logger, IMessageBus bus)
    {
        _logger = logger;
        _bus = bus;
    }


    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var request = new RenewJobExecutionRequestEvent(DateTime.Now.Day);
        
        await _bus.SendAsync(request);
    }
}
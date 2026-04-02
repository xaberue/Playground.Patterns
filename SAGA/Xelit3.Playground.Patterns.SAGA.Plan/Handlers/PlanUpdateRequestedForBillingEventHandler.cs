using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;

namespace Xelit3.Playground.Patterns.SAGA.Plans.Handlers;

public class PlanUpdateRequestedForBillingEventHandler
{

    private readonly ILogger<PlanUpdateRequestedForBillingEventHandler> _logger;
    private readonly IMessageBus _bus;


    public PlanUpdateRequestedForBillingEventHandler(ILogger<PlanUpdateRequestedForBillingEventHandler> logger, IMessageBus bus)
    {
        _logger = logger;
        _bus = bus;
    }


    public async Task HandleAsync(PlanUpdateRequestedForBillingEvent request)
    {
        _logger.LogInformation("Received PlanUpdateRequestedForBillingEvent: {Request}", request);

        //Logic to update the plan accordingly
        var status = request.Successful ? PlanStatus.Active : PlanStatus.Cancelled;
        //
        var planReadyRequest = new PlanUpdateReadyForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, status);

        await _bus.SendAsync(planReadyRequest);
    }
}

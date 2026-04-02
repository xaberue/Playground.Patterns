using JasperFx.Events;
using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class PlanUpdateReadyForBillingEventHandler
{

    private readonly ILogger<PlanUpdateReadyForBillingEventHandler> _logger;
    private readonly BillingDbContext _billingDbContext;
    private readonly IMessageBus _bus;


    public PlanUpdateReadyForBillingEventHandler(ILogger<PlanUpdateReadyForBillingEventHandler> logger, BillingDbContext billingDbContext, IMessageBus bus)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
        _bus = bus;
    }


    public async Task HandleAsync(PlanUpdateReadyForBillingEvent request)
    {
        var entity = _billingDbContext.UserBillingSagas.FirstOrDefault(x => x.JobId == request.JobId && x.UserId == request.UserId && x.PlanId == request.PlanId);
        entity?.MarkPlanUpdated(request.Status);

        var planCompletedRequest = new PlanCompletedForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId);

        await _bus.SendAsync(planCompletedRequest);

        await _billingDbContext.SaveChangesAsync();
    }
}

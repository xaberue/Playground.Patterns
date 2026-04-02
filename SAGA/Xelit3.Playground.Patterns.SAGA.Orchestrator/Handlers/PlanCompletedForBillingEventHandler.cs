using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class PlanCompletedForBillingEventHandler
{

    private ILogger<PlanCompletedForBillingEventHandler> _logger;
    private BillingDbContext _billingDbContext;


    public PlanCompletedForBillingEventHandler(ILogger<PlanCompletedForBillingEventHandler> logger, BillingDbContext billingDbContext)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
    }


    public async Task HandleAsync(PlanCompletedForBillingEvent request)
    {
        var entity = _billingDbContext.UserBillingSagas.FirstOrDefault(x => x.JobId == request.JobId && x.UserId == request.UserId && x.PlanId == request.PlanId);
        entity?.MarkCompleted();

        await _billingDbContext.SaveChangesAsync();
    }
}

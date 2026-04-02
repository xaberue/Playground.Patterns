using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class PlanReadyForBillingHandler
{

    private readonly ILogger<PlanReadyForBillingHandler> _logger;
    private readonly BillingDbContext _billingDbContext;
    private readonly IMessageBus _bus;


    public PlanReadyForBillingHandler(ILogger<PlanReadyForBillingHandler> logger, BillingDbContext billingDbContext, IMessageBus bus)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
        _bus = bus;
    }


    public async Task HandleAsync(PlanReadyForBillingEvent request)
    {
        var entity = new UserBillingSaga(request.JobId, request.PlanId, request.UserId);

        await _billingDbContext.UserBillingSagas.AddAsync(entity);

        var discountRequest = new DiscountRequestedForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId);

        await _bus.SendAsync(discountRequest);

        await _billingDbContext.SaveChangesAsync();
    }
}

using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class DiscountReadyForBillingEventHandler
{

    private readonly ILogger<DiscountReadyForBillingEventHandler> _logger;
    private readonly BillingDbContext _billingDbContext;
    private readonly IMessageBus _bus;


    public DiscountReadyForBillingEventHandler(ILogger<DiscountReadyForBillingEventHandler> logger, BillingDbContext billingDbContext, IMessageBus bus)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
        _bus = bus;
    }


    public async Task HandleAsync(DiscountReadyForBillingEvent request)
    {
        var entity = _billingDbContext.UserBillingSagas.FirstOrDefault(x => x.JobId == request.JobId && x.UserId == request.UserId && x.PlanId == request.PlanId);
        entity?.MarkDiscountCalculated(request.Discount);

        var paymentRequest = new AmountCalculationRequestedForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, request.Discount);

        await _bus.SendAsync(paymentRequest);

        await _billingDbContext.SaveChangesAsync();
    }
}

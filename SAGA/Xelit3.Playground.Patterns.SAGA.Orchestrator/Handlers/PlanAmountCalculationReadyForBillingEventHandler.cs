using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class PlanAmountCalculationReadyForBillingEventHandler
{

    private readonly ILogger<PlanAmountCalculationReadyForBillingEventHandler> _logger;
    private readonly BillingDbContext _billingDbContext;
    private readonly IMessageBus _bus;


    public PlanAmountCalculationReadyForBillingEventHandler(ILogger<PlanAmountCalculationReadyForBillingEventHandler> logger, BillingDbContext billingDbContext, IMessageBus bus)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
        _bus = bus;
    }


    public async Task HandleAsync(PlanAmountCalculationReadyForBillingEvent request)
    {
        _logger.LogInformation("Received PaymentCalculationReadyForBillingEvent: {Request}", request);

        var entity = _billingDbContext.UserBillingSagas.FirstOrDefault(x => x.JobId == request.JobId && x.UserId == request.UserId && x.PlanId == request.PlanId);
        entity?.MarkAmountCalculated(request.Amount);

        var paymentRequest = new PaymentRequestedForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, request.Discount, request.Amount);

        await _bus.SendAsync(paymentRequest);

        await _billingDbContext.SaveChangesAsync();
    }

}

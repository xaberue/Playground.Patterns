using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Handlers;

public class PaymentResultReadyForBillingEventHandler
{

    private readonly ILogger<PaymentResultReadyForBillingEvent> _logger;
    private readonly BillingDbContext _billingDbContext;
    private readonly IMessageBus _bus;


    public PaymentResultReadyForBillingEventHandler(ILogger<PaymentResultReadyForBillingEvent> logger, BillingDbContext billingDbContext, IMessageBus bus)
    {
        _logger = logger;
        _billingDbContext = billingDbContext;
        _bus = bus;
    }


    public async Task HandleAsync(PaymentResultReadyForBillingEvent request)
    {
        _logger.LogInformation("Received PaymentResultReadyForBillingEvent: {Request}", request);

        var entity = _billingDbContext.UserBillingSagas.FirstOrDefault(x => x.JobId == request.JobId && x.UserId == request.UserId && x.PlanId == request.PlanId);
        entity?.MarkPaymentProcessed(request.TransactionId.ToString(), request.Successful, request.ResultCode);

        var paymentRequest = new PlanUpdateRequestedForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, request.Successful);

        await _bus.SendAsync(paymentRequest);

        await _billingDbContext.SaveChangesAsync();
    }

}

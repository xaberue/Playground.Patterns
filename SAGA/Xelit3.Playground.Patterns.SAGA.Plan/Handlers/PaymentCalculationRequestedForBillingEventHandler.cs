using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Plans.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Plans.Handlers;

public class PaymentCalculationRequestedForBillingEventHandler
{

    private readonly ILogger<PaymentCalculationRequestedForBillingEventHandler> _logger;
    private readonly IMessageBus _bus;


    public PaymentCalculationRequestedForBillingEventHandler(ILogger<PaymentCalculationRequestedForBillingEventHandler> logger, IMessageBus bus)
    {
        _logger = logger;
        _bus = bus;
    }


    public async Task HandleAsync(PaymentCalculationRequestedForBillingEvent request)
    {
        _logger.LogInformation("Received PaymentCalculationRequestedForBillingEvent: {PaymentCalculationRequestedForBillingEvent}", request);

        //Logic to calculate the payment based on the plan and discount
        var paymentReadyRequest = new PaymentCalculationReadyForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, request.Discount, 100);

        await _bus.SendAsync(paymentReadyRequest);
    }
}

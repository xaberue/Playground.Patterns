using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;

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


    public async Task HandleAsync(AmountCalculationRequestedForBillingEvent request)
    {
        _logger.LogInformation("Received PaymentCalculationRequestedForBillingEvent: {PaymentCalculationRequestedForBillingEvent}", request);

        //Logic to calculate the payment based on the plan and discount
        var amount = 100 - request.Discount; // This is just a placeholder. You would replace this with your actual calculation logic.
        //
        var paymentReadyRequest = new PaymentCalculationReadyForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, request.Discount, amount);
        

        await _bus.SendAsync(paymentReadyRequest);
    }
}

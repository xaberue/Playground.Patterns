using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;

namespace Xelit3.Playground.Patterns.SAGA.Payments.Features.GetCurrentPlans;

public class PaymentRequestedForBillingEventHandler
{

    private readonly ILogger<PaymentRequestedForBillingEventHandler> _logger;
    private readonly IMessageBus _bus;


    public PaymentRequestedForBillingEventHandler(ILogger<PaymentRequestedForBillingEventHandler> logger, IMessageBus bus)
    {
        _logger = logger;
        _bus = bus;
    }


    public async Task HandleAsync(PaymentRequestedForBillingEvent request)
    {
        _logger.LogInformation("Received PaymentRequestedForBillingEvent: {Request}", request);

        //Logic to execute the payment process, e.g. call to external payment provider, etc. Randomize successful and failed payments for testing purposes.
        var randomNumber = new Random().Next(1, 99);
        var paymentSuccessful = randomNumber < 90;
        var errorCode = randomNumber > 90 ? 500 + randomNumber : 0;
        //
        var paymentResultEvent = new PaymentResultReadyForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, paymentSuccessful, errorCode);

        await _bus.SendAsync(paymentResultEvent);
    }
}

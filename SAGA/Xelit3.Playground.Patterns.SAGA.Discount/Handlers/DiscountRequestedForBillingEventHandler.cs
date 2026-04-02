using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;

namespace Xelit3.Playground.Patterns.SAGA.Discounts.Handlers;

public class DiscountRequestedForBillingEventHandler
{
    private readonly ILogger<DiscountRequestedForBillingEventHandler> _logger;
    private readonly IMessageBus _bus;


    public DiscountRequestedForBillingEventHandler(ILogger<DiscountRequestedForBillingEventHandler> logger, IMessageBus bus)
    {
        _logger = logger;
        _bus = bus;
    }


    public async Task HandleAsync(DiscountRequestedForBillingEvent request)
    {
        _logger.LogInformation("Received DiscountRequestedForBillingEvent: {Event}", request);

        var discount = Math.Max(0, Random.Shared.Next(-100, 100));
        _logger.LogInformation("Discount found for {UserId} : {Event}", request.UserId, discount);

        var discountEvent = new DiscountReadyForBillingEvent(request.JobId, request.CorrelationId, request.PlanId, request.UserId, discount);

        await _bus.PublishAsync(discountEvent);
    }
}

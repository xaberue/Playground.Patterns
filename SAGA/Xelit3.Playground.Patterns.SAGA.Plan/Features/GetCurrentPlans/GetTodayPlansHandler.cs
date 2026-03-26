using Wolverine;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Plans.Infrastructure;

namespace Xelit3.Playground.Patterns.SAGA.Plans.Features.GetCurrentPlans;

public class GetTodayPlansHandler
{

    private readonly ILogger<GetTodayPlansHandler> _logger;
    private readonly PlanRepository _planRepository;
    private readonly IMessageBus _bus;


    public GetTodayPlansHandler(ILogger<GetTodayPlansHandler> logger, PlanRepository planRepository, IMessageBus bus)
    {
        _logger = logger;
        _planRepository = planRepository;
        _bus = bus;
    }


    public async Task HandleAsync(BillingJobExecutionRequestEvent request)
    {
        _logger.LogInformation("Received BillingJobExecutionRequestEvent for day {Day}", request.Day);
        
        var plans = _planRepository.GetAll(request.Day);

        foreach (var plan in plans)
        {
            _logger.LogInformation("Publishing PlanReadyForBillingEvent for plan {PlanId} on day {Day}", plan.Id, request.Day);
            await _bus.PublishAsync(new PlanReadyForBillingEvent(plan.Id, plan.UserId));
        }
    }
}

namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record DiscountRequestedForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId);
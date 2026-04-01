namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record DiscountReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, decimal Discount);

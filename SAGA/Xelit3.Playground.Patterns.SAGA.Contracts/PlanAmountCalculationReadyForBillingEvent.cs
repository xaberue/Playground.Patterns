namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanAmountCalculationReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, decimal Discount, decimal Amount);

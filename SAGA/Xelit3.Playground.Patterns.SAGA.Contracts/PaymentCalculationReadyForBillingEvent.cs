namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PaymentCalculationReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, decimal Discount, decimal Amount);

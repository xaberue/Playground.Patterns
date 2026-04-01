namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PaymentCalculationRequestedForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, decimal Discount);

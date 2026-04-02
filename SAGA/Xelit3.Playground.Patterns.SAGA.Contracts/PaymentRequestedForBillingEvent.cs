namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PaymentRequestedForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, decimal Discount, decimal Amount);

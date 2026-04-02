namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PaymentResultReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, bool Successful, int? ErrorCode = 0);

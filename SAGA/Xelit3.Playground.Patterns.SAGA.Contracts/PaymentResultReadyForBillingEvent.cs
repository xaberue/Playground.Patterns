namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PaymentResultReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, Guid TransactionId, bool Successful, int ResultCode);

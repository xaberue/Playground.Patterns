namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanUpdateRequestedForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, bool Successful);

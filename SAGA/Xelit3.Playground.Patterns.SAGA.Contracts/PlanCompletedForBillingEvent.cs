namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanCompletedForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId);
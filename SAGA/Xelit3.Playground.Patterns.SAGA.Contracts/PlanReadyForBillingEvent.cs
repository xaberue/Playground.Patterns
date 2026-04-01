namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId);
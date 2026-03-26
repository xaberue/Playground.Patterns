namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanReadyForBillingEvent(Guid PlanId, Guid UserId);
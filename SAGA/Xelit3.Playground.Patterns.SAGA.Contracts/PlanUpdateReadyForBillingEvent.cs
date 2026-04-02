namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record PlanUpdateReadyForBillingEvent(Guid JobId, Guid CorrelationId, Guid PlanId, Guid UserId, PlanStatus Status);


public enum PlanStatus
{
    Active,
    Cancelled
}
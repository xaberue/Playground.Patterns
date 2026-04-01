namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record BillingPlansRequestedEvent(Guid JobId, int Day);

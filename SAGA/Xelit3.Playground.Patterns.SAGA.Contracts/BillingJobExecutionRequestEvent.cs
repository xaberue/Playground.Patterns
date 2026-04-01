namespace Xelit3.Playground.Patterns.SAGA.Contracts;

public record BillingJobExecutionRequestEvent(Guid JobId, int Day);

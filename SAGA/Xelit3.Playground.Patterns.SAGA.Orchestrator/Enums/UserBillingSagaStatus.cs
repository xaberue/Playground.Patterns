namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Enums;

public enum UserBillingSagaStatus
{
    Pending = 0,
    DiscountCalculated = 1,
    PaymentProcessed = 2,
    PlanUpdated = 3,
    Completed = 4,
    Failed = 5
}

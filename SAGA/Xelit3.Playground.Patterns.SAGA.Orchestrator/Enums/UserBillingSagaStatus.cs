namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Enums;

public enum UserBillingSagaStatus
{
    Pending = 0,
    DiscountCalculated = 1,
    AmountCalculated = 2,
    PaymentProcessed = 3,
    PlanUpdated = 4,
    Completed = 5,
    Failed = 6
}

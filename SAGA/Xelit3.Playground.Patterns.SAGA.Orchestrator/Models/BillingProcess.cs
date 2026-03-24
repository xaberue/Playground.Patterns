namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

public class BillingProcess
{
    public Guid Id { get; init; }
    public DateOnly BillingDate { get; init; }
    public BillingProcessStatus Status { get; init; }
    public int TotalUsersToProcess { get; init; }
    public int DiscountsCalculated { get; init; }
    public int PaymentsProcessed { get; init; }
    public int Completed { get; init; }
    public int Failed { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? StartedAt { get; init; }
    public DateTime? FinishedAt { get; init; }


    public BillingProcess()
    {
        Id = Guid.NewGuid();
        BillingDate = DateOnly.FromDateTime(DateTime.UtcNow);

        Status = BillingProcessStatus.Pending;

        TotalUsersToProcess = 0;

        DiscountsCalculated = 0;
        PaymentsProcessed = 0;
        Completed = 0;
        Failed = 0;

        CreatedAt = DateTime.UtcNow;
        StartedAt = null;
        FinishedAt = null;
    }

}

public enum BillingProcessStatus
{
    Pending = 0,
    Running = 1,
    Completed = 2,
    Failed = 3
}

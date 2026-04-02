using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Enums;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

public class UserBillingSaga
{
    public Guid Id { get; private set; }

    public Guid JobId { get; private set; }
    public Guid PlanId { get; private set; }
    public Guid UserId { get; private set; }

    public UserBillingSagaStatus Status { get; private set; }

    public int RetryCount { get; private set; }
    public string? LastError { get; private set; }

    public decimal? DiscountAmount { get; private set; }
    public decimal? PaymentAmount { get; private set; }
    public string? PaymentTransactionId { get; private set; }
    public bool? PaymentSuccessful { get; private set; }
    public int? PaymentResultCode { get; private set; }
    public PlanStatus? PlanStatus { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private UserBillingSaga() { }

    public UserBillingSaga(Guid jobId, Guid planId, Guid userId)
    {
        Id = Guid.NewGuid();
        JobId = jobId;
        PlanId = planId;
        UserId = userId;

        Status = UserBillingSagaStatus.Pending;
        RetryCount = 0;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void MarkDiscountCalculated(decimal discount)
    {
        if (Status != UserBillingSagaStatus.Pending)
            throw new InvalidOperationException("Invalid state transition.");

        DiscountAmount = discount;
        Status = UserBillingSagaStatus.DiscountCalculated;
        Touch();
    }

    public void MarkAmountCalculated(decimal amount)
    {
        if (Status != UserBillingSagaStatus.DiscountCalculated)
            throw new InvalidOperationException("Invalid state transition.");

        PaymentAmount = amount;
        Status = UserBillingSagaStatus.AmountCalculated;
        Touch();
    }

    public void MarkPaymentProcessed(string transactionId, bool successful, int resultCode)
    {
        if (Status != UserBillingSagaStatus.AmountCalculated)
            throw new InvalidOperationException("Invalid state transition.");

        PaymentTransactionId = transactionId;
        PaymentSuccessful = successful;
        PaymentResultCode = resultCode;
        Status = UserBillingSagaStatus.PaymentProcessed;
        Touch();
    }

    public void MarkPlanUpdated(PlanStatus planStatus)
    {
        if (Status != UserBillingSagaStatus.PaymentProcessed)
            throw new InvalidOperationException("Invalid state transition.");

        PlanStatus = planStatus;
        Status = UserBillingSagaStatus.PlanUpdated;
        Touch();
    }

    public void MarkCompleted()
    {
        if (Status != UserBillingSagaStatus.PlanUpdated)
            throw new InvalidOperationException("Invalid state transition.");

        Status = UserBillingSagaStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        Touch();
    }

    public void Fail(string error)
    {
        RetryCount++;
        LastError = error;
        Status = UserBillingSagaStatus.Failed;
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

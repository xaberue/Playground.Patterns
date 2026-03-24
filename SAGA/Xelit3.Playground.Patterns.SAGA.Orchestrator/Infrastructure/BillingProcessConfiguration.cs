using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

public class BillingProcessConfiguration : IEntityTypeConfiguration<BillingProcess>
{
    public void Configure(EntityTypeBuilder<BillingProcess> builder)
    {
        builder.ToTable("BillingProcesses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BillingDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.TotalUsersToProcess)
            .IsRequired();

        builder.Property(x => x.DiscountsCalculated)
            .IsRequired();

        builder.Property(x => x.PaymentsProcessed)
            .IsRequired();

        builder.Property(x => x.Completed)
            .IsRequired();

        builder.Property(x => x.Failed)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.StartedAt)
            .HasColumnType("datetime2");

        builder.Property(x => x.FinishedAt)
            .HasColumnType("datetime2");

        builder.HasIndex(x => x.BillingDate).IsUnique();
        builder.HasIndex(x => x.Status);
    }
}

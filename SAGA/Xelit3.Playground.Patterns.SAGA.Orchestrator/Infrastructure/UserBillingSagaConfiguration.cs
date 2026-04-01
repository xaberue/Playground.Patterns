using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

public class UserBillingSagaConfiguration : IEntityTypeConfiguration<UserBillingSaga>
{
    public void Configure(EntityTypeBuilder<UserBillingSaga> builder)
    {
        builder.ToTable("UserBillingSagas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.JobId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.RetryCount).IsRequired();

        builder.Property(x => x.LastError)
            .HasMaxLength(2000);

        builder.Property(x => x.DiscountAmount)
            .HasColumnType("decimal(10,2)");

        builder.Property(x => x.PaymentTransactionId)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .HasColumnType("datetime2");

        // Índices clave
        builder.HasIndex(x => x.JobId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Status);
    }
}

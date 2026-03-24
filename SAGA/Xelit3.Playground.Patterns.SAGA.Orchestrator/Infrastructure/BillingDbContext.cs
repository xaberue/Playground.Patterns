using Microsoft.EntityFrameworkCore;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Models;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

public class BillingDbContext : DbContext
{
    public DbSet<BillingProcess> BillingProcesses => Set<BillingProcess>();


    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BillingProcessConfiguration());
    }
}

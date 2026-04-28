using CleanDddHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanDddHexagonal.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<CustomerAccount> Customers => Set<CustomerAccount>();
    public DbSet<InternalUsageRecord> InternalUsageRecords => Set<InternalUsageRecord>();
    public DbSet<ExternalUsageSnapshot> ExternalUsageSnapshots => Set<ExternalUsageSnapshot>();
    public DbSet<ReconciliationIssue> ReconciliationIssues => Set<ReconciliationIssue>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerAccount>(entity =>
        {
            entity.ToTable("customers");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(120);
            entity.Property(x => x.CreatedAtUtc).IsRequired();
        });

        modelBuilder.Entity<InternalUsageRecord>(entity =>
        {
            entity.ToTable("internal_usage_records");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ServiceSku).IsRequired().HasMaxLength(80);
            entity.Property(x => x.Currency).IsRequired().HasMaxLength(3);
            entity.Property(x => x.MonthlyCost).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ExternalUsageSnapshot>(entity =>
        {
            entity.ToTable("external_usage_snapshots");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ServiceSku).IsRequired().HasMaxLength(80);
            entity.Property(x => x.Currency).IsRequired().HasMaxLength(3);
            entity.Property(x => x.MonthlyCost).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ReconciliationIssue>(entity =>
        {
            entity.ToTable("reconciliation_issues");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ServiceSku).IsRequired().HasMaxLength(80);
            entity.Property(x => x.Description).IsRequired().HasMaxLength(500);
            entity.Property(x => x.ResolutionNote).HasMaxLength(500);
            entity.Ignore(x => x.DomainEvents);
        });
    }
}

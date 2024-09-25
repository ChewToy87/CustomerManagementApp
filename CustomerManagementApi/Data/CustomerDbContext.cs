using CustomerManagementApi.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .Property(b => b.Created)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Customer>()
            .Property(b => b.Updated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Customer>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Updated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

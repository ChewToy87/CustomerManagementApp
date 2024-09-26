using CustomerManagementApi.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default values for Created and Updated columns
        modelBuilder.Entity<Customer>()
            .Property(b => b.Created)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Customer>()
            .Property(b => b.Updated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Seed data: Add initial customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Created = DateTime.Now,
                Updated = DateTime.Now
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                Surname = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "0987654321",
                Created = DateTime.Now,
                Updated = DateTime.Now
            },
            new Customer
            {
                Id = 3,
                FirstName = "Bob",
                Surname = "Brown",
                Email = "bob.brown@example.com",
                PhoneNumber = "1122334455",
                Created = DateTime.Now,
                Updated = DateTime.Now
            },
            new Customer
            {
                Id = 4,
                FirstName = "Alice",
                Surname = "Johnson",
                Email = "alice.johnson@example.com",
                PhoneNumber = "2233445566",
                Created = DateTime.Now,
                Updated = DateTime.Now
            },
            new Customer
            {
                Id = 5,
                FirstName = "Charlie",
                Surname = "White",
                Email = "charlie.white@example.com",
                PhoneNumber = "3344556677",
                Created = DateTime.Now,
                Updated = DateTime.Now
            }
        );
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

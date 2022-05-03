using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<PhotoEntity> Photos { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var modelBaseTypes = modelBuilder.Model.GetEntityTypes()
            .Select(et => et.ClrType)
            .Where(x => x.BaseType == typeof(EntityBase)).ToArray();
        foreach (var type in modelBaseTypes)
            modelBuilder.Entity(type, entity =>
            {
                entity.Property(nameof(EntityBase.InsertTime))
                    .HasDefaultValueSql("current_timestamp()");
                entity.Property(nameof(EntityBase.Key))
                    .HasDefaultValueSql("UUID()");
            });
    }
}
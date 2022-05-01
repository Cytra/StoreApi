using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext: DbContext
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
}
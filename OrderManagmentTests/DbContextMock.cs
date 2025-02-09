using Microsoft.EntityFrameworkCore;
using OrderManagment.Database;
using System.Reflection.Emit;

namespace OrderManagmentTests;
public class DbContextMock : OrderDbContext
{
    public DbContextMock(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

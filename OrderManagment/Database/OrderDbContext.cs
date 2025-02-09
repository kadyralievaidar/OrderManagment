using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderManagment.Feautures.Orders.Models;
using OrderManagment.Feautures.Payment.Models;
using OrderManagment.Feautures.Products.Models;

namespace OrderManagment.Database;
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    ///     Apply configurations 
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}

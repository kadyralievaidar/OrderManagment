using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OrderManagment.Database;
using OrderManagment.Database.UoW;
using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Models;

namespace OrderManagmentTests;
internal class UnitOfWorkFactory : IDisposable
{
    internal SqliteConnection _connection;

    internal DbContextMock _context;

    internal UnitOfWorkFactory()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        _context = new DbContextMock(options);

        ClearData();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection?.Dispose();
        _context.Database.EnsureDeleted();
        _context?.Dispose();
    }

    internal void AddData<T>(List<T> data) where T : BaseModel
    {
        var type = typeof(DbContextMock);
        var prop = type.GetProperties().FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>));
        var getter = prop.GetGetMethod();

        var dbSet = (DbSet<T>)getter.Invoke(_context, null);
        dbSet.AddRange(data);

        _context.SaveChanges();
    }

    private void ClearData()
    {
        if (_context.Database.EnsureCreated())
        {
            foreach (var entity in _context.Products)
            {
                _context.Products.Remove(entity);
                _context.SaveChanges();
            }
            foreach (var entity in _context.Payments)
            {
                _context.Payments.Remove(entity);
                _context.SaveChanges();
            }
            foreach (var entity in _context.Orders)
            {
                _context.Orders.Remove(entity);
                _context.SaveChanges();
            }

            _context.SaveChanges();
        }
    }

    internal IUnitOfWork CreateUnitOfWork(bool includeClientGroupToContext)
    {

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>())
            .Build();

        var dbTransaction = new Mock<DbTransaction>();
        var paymentRepository = new Mock<PaymentRepository>();
        var productRepository = new Mock<ProductRepository>();
        var orderRepository = new Mock<OrderRepository>();

        return new UnitOfWork(dbTransaction.Object, paymentRepository.Object, productRepository.Object, orderRepository.Object, _context);
    }
}
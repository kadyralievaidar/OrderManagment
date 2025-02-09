using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagment.Database;
using OrderManagment.Database.UoW;
using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Feautures.Orders.Interfaces;
using OrderManagment.Feautures.Orders.Services;
using OrderManagment.Feautures.Products.Interfaces;
using OrderManagment.Feautures.Products.Services;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) => 
    {
        var test = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDbTransaction, DbTransaction>();
    })
    .Build();

host.Run();
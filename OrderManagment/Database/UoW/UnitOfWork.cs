using OrderManagment.Database.UoW.Repositories;

namespace OrderManagment.Database.UoW;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbTransaction _dbTransaction;
    private readonly Lazy<IPaymentRepository> _paymentRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;

    private readonly OrderDbContext _context;

    public UnitOfWork
        (IDbTransaction dbTransaction, 
        IPaymentRepository paymentRepository, 
        IProductRepository productRepository, 
        IOrderRepository orderRepository,
        OrderDbContext context)
    {
        _dbTransaction = dbTransaction;
        _paymentRepository = new Lazy<IPaymentRepository>(paymentRepository);
        _productRepository = new Lazy<IProductRepository>(productRepository);
        _orderRepository = new Lazy<IOrderRepository>(orderRepository);
        _context = context;
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
    public IDbTransaction BeginTransaction()
    {
        _dbTransaction.BeginTransaction();
        return _dbTransaction;
    }
    public IPaymentRepository PaymentRepository => _paymentRepository.Value;

    public IProductRepository ProductRepository => _productRepository.Value;
    public IOrderRepository OrderRepository => _orderRepository.Value;


    private void Dispose(bool disposing)
    {
        if (disposing)
            _context.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

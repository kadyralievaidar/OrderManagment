using OrderManagment.Database.UoW.Repositories;

namespace OrderManagment.Database.UoW;
public interface IUnitOfWork
{
    IPaymentRepository PaymentRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IDbTransaction BeginTransaction();
}

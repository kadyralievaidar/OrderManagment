using OrderManagment.Database.UoW;
using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Feautures.Discount.Interfaces;
using OrderManagment.Feautures.Notification.Interfaces;
using OrderManagment.Feautures.Orders.Interfaces;
using OrderManagment.Feautures.Orders.Models;
using OrderManagment.Feautures.Payment.Interfaces;

namespace OrderManagment.Feautures.Orders.Services;
public class OrderService(IOrderRepository repository, IUnitOfWork unitOfWork, IPayment payment) : IOrderService
{
    private IEnumerable<IObserver> _observers = new List<IObserver>();

    public async Task ProccesOrder(Order order, IEnumerable<IObserver> observers, IDiscountStrategy discountStrategy, CancellationToken cancellationToken)
    {
        using var scope = unitOfWork.BeginTransaction();
        try
        {
            var orderEntity = repository.GetById(order.Id);
            if (orderEntity == null)
            {
                orderEntity = order;
                orderEntity.Price = discountStrategy.ApplyDiscount(orderEntity.Price);
                _observers = observers;
                var result = payment.Pay(orderEntity.Price);
                if (0 == result)
                    throw new Exception();
                await repository.AddAsync(orderEntity);
            }
            await scope.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await scope.RollbackAsync(cancellationToken);
            throw;
        }
    }
}

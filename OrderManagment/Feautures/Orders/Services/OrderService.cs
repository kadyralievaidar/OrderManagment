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
    private readonly List<IObserver> _observers = new();


    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers(Order order)
    {
        foreach (var observer in _observers)
        {
            observer.Update(order.OrderStatus, order.Id);
        }
    }
    public async Task ProccesOrder(Order order, IDiscountStrategy discountStrategy, CancellationToken cancellationToken)
    {
        using var scope = unitOfWork.BeginTransaction();
        try
        {
            var orderEntity = repository.GetById(order.Id);
            if (orderEntity == null)
            {
                orderEntity = order;
                orderEntity.Price = discountStrategy.ApplyDiscount(orderEntity.Price);
                var result = payment.Pay(orderEntity.Price);
                if (0 == result)
                    throw new Exception();
                await repository.AddAsync(orderEntity);
                NotifyObservers(orderEntity);
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

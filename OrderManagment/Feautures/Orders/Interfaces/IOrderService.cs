using OrderManagment.Feautures.Discount.Interfaces;
using OrderManagment.Feautures.Notification.Interfaces;
using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Feautures.Orders.Interfaces;
public interface IOrderService
{
    void RegisterObserver(IObserver observer);
    void UnregisterObserver(IObserver observer);
    Task ProccesOrder(Order order, IDiscountStrategy discountStrategy,CancellationToken cancellation);
}

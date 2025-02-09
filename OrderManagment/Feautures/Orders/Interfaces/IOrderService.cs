using OrderManagment.Feautures.Discount.Interfaces;
using OrderManagment.Feautures.Notification.Interfaces;
using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Feautures.Orders.Interfaces;
public interface IOrderService
{
    Task ProccesOrder(Order order, IEnumerable<IObserver> observer, IDiscountStrategy discountStrategy,CancellationToken cancellation);
}

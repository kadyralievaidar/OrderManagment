using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Feautures.Notification.Interfaces;
public interface IObserver
{
    int Update(OrderStatus orderStatus, Guid OrderId);
}
